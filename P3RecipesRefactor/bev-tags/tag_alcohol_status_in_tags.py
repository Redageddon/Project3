#%% [markdown]
# # Tag Beverage Alcohol Status (Manual Name Lists → **tags only**)
# This Jupyter-style script (for VS Code) updates **tags** based on *exact recipe names* you list.
# - It **does not** touch `mealType`.
# - It ensures exactly one of `Alcoholic` or `Non-Alcoholic` is present in **tags** for listed recipes.
# - It writes:
#     - `newrecipes.json` (updated copy)
#     - `changed_recipes.csv` (audit of what changed)
# - It also reports any recipes whose **mealType** contains `"Beverage"` but are **not** in your lists,
#   so you can add them if needed.

#%% Config: paths & manual name lists
from pathlib import Path
import json
from typing import List, Dict

# Paths (relative to where you run this file)
INPUT_PATH = Path("recipes.json")
OUTPUT_PATH = Path("NewTags.json")
CHANGES_CSV_PATH = Path("changed_recipes.csv")

# ---- EDIT THESE LISTS AS NEEDED (exact title match, case-insensitive) ----
ALCOHOLIC_NAMES = [
    "Brazilian Caipirinha",
    "Classic Mojito",
    "Negroni",
    "Paloma",
    "Old Fashioned",
    "Classic Margarita",
    "Whiskey Sour",
    "French 75",
    "Espresso Martini",
    "Manhattan",
    "Moscow Mule",
    "Mai Tai (Trader Vic-style)",
    "Pisco Sour",
    "Boulevardier",
    "Aperol Spritz",
    "Dark 'n' Stormy",
    "Spanish Red Sangria",
    "Michelada",
]

NON_ALCOHOLIC_NAMES = [
    "Mango Lassi",
    "Blueberry Banana Smoothie",
    "Pineapple Coconut Smoothie",
    "Agua de Jamaica (Hibiscus Tea)",
    "Iced Matcha Latte",
    "Thai Iced Tea",
    "Horchata (Mexican Rice Drink)",
    "Arnold Palmer",
    "Shirley Temple",
    "Vietnamese Iced Coffee (Cà phê sữa đá)",
    "Watermelon Agua Fresca",
    "Homemade Ginger Beer (Non-Alcoholic)",
    "Classic Hot Chocolate (Parisian-Style)",
    "Iced Chai Latte",
    "Golden Milk (Turmeric Latte)",
]

INPUT_PATH, OUTPUT_PATH, CHANGES_CSV_PATH

#%% Load recipes.json
if not INPUT_PATH.exists():
    raise FileNotFoundError(f"Input not found: {INPUT_PATH.resolve()}")

data = json.loads(INPUT_PATH.read_text(encoding="utf-8"))
recipes: List[Dict] = data.get("recipes", [])
print(f"Loaded {len(recipes)} recipes from: {INPUT_PATH.resolve()}")

#%% Helpers for name matching & tag normalization
def _lower(s: str) -> str:
    return (s or "").strip().lower()

ALC_SET  = {_lower(n) for n in ALCOHOLIC_NAMES}
NON_SET  = {_lower(n) for n in NON_ALCOHOLIC_NAMES}

def classify_by_name(recipe_name: str) -> str | None:
    """
    Return 'Alcoholic' | 'Non-Alcoholic' if the name is in the manual lists; else None.
    """
    n = _lower(recipe_name)
    if n in ALC_SET: return "Alcoholic"
    if n in NON_SET: return "Non-Alcoholic"
    return None

def normalize_tags(tags: List[str]) -> List[str]:
    """
    Deduplicate tags case-insensitively while preserving first occurrence order.
    """
    out, seen = [], set()
    for t in tags:
        key = t.lower()
        if key not in seen:
            seen.add(key)
            out.append(t)
    return out

def lower_list(x): 
    return [s.lower() for s in x]

#%% Process & track changes (tags only)
from copy import deepcopy
import csv

updated_data = deepcopy(data)
updated_recipes = updated_data.get("recipes", [])

changes: List[Dict] = []  # {index, name, before_tags, after_tags}
unlisted_beverages: List[str] = []  # recipes with mealType containing 'Beverage' but not in our lists

for idx, r in enumerate(updated_recipes):
    name = r.get("name", "(unnamed)")
    before_tags = list(r.get("tags", []))  # copy
    meal_type = r.get("mealType", [])
    class_flag = classify_by_name(name)

    if class_flag is not None:
        tags = list(r.get("tags", []))

        # Remove any existing Alcoholic/Non-Alcoholic (case-insensitive)
        tags = [t for t in tags if t.lower() not in {"alcoholic", "non-alcoholic"}]

        # Add the correct flag
        tags.append(class_flag)

        # Normalize tags
        r["tags"] = normalize_tags(tags)

        after_tags = r["tags"]
        if lower_list(before_tags) != lower_list(after_tags):
            changes.append({
                "index": idx,
                "name": name,
                "before_tags": before_tags,
                "after_tags": after_tags
            })

    # FYI: beverages in mealType not covered by lists
    elif "beverage" in [m.lower() for m in meal_type]:
        unlisted_beverages.append(name)

len(changes), len(unlisted_beverages)

#%% Review changes (pretty print)
if changes:
    print("Changed recipes (Name | Old Tags -> New Tags):")
    for c in changes:
        print(f" - {c['name']} | [{', '.join(c['before_tags'])}] -> [{', '.join(c['after_tags'])}]")
    print(f"\nTotal changed: {len(changes)}")
else:
    print("No tag changes made by manual lists.")

if unlisted_beverages:
    print("\nBeverages in data NOT covered by your manual lists (consider adding):")
    for n in sorted(set(unlisted_beverages), key=str.lower):
        print(f" - {n}")
else:
    print("\nAll beverages in data are covered by your lists. ✅")

#%% Save: newrecipes.json + changed_recipes.csv
# JSON
OUTPUT_PATH.write_text(json.dumps(updated_data, indent=2, ensure_ascii=False), encoding="utf-8")
print(f"\nWrote updated JSON → {OUTPUT_PATH.resolve()}")

# CSV
with CHANGES_CSV_PATH.open("w", encoding="utf-8", newline="") as f:
    w = csv.writer(f)
    w.writerow(["index","name","before_tags","after_tags"])
    for c in changes:
        w.writerow([c["index"], c["name"], "; ".join(c["before_tags"]), "; ".join(c["after_tags"])])
print(f"Wrote change list CSV → {CHANGES_CSV_PATH.resolve()}")

#%% Sanity checks (only manual-listed items changed; mealType untouched)
changed_names = {c["name"].lower() for c in changes}

# 1) Every changed item must be from the manual lists
assert all(n in (ALC_SET | NON_SET) for n in changed_names), "A non-listed recipe was changed."

# 2) Each changed recipe must have exactly one of Alcoholic/Non-Alcoholic in TAGS now
for c in changes:
    tags_low = [t.lower() for t in c["after_tags"]]
    count = tags_low.count("alcoholic") + tags_low.count("non-alcoholic")
    assert count == 1, f"Expected exactly one alcoholic tag in {c['name']}"

# 3) mealType array lengths unchanged (we didn't touch them)
orig_mealtype_counts = [len(r.get("mealType", [])) for r in data.get("recipes", [])]
new_mealtype_counts  = [len(r.get("mealType", [])) for r in updated_data.get("recipes", [])]
assert orig_mealtype_counts == new_mealtype_counts, "mealType changed—should not have."

print("Sanity checks passed ✅  (tags updated, mealType untouched)")
