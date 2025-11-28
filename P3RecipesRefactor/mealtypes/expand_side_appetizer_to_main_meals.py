#%% Imports and file paths
import json
from pathlib import Path

# Easy-to-read paths
input_path = Path("NewTags.json")
output_path = Path("newrecipes_meals_expanded.json")

assert input_path.exists(), f"Input file not found: {input_path.resolve()}"


#%% Load JSON
with input_path.open("r", encoding="utf-8") as f:
    data = json.load(f)

# Guard: expect a top-level dict with "recipes": [...]
recipes = data.get("recipes", [])
print(f"Loaded {len(recipes)} recipes from {input_path}")


#%% Helper functions
def normalize_meal_types(meal_types):
    """
    Return a copy of 'meal_types' with:
      - 'Appetizer' and 'Side Dish' removed
      - 'Breakfast', 'Lunch', 'Dinner' added (if not already present)
      - No duplicates, stable readable order:
          Breakfast, Lunch, Dinner, then the remaining original items
    """
    if not isinstance(meal_types, list):
        return meal_types  # If it's missing or malformed, leave as-is.

    # Copy to avoid mutating the original list during iteration
    original = list(meal_types)

    # Remove the ones we’re replacing
    to_remove = {"Appetizer", "Side Dish"}
    filtered = [m for m in original if m not in to_remove]

    # Ensure the three base meal slots exist
    base = ["Breakfast", "Lunch", "Dinner"]
    # Start with base (no dupes), then the filtered leftovers that aren't base
    new_list = []
    for b in base:
        if b in filtered or b in original:
            # If user already had it, it'll be included below; we want it once at the front.
            pass
    # Add base items (only once)
    for b in base:
        if b not in filtered:
            # If missing, add it
            new_list.append(b)
    # Now ensure any base that was already present is also represented exactly once.
    # Combine base (already added if missing) with filtered, but skip duplicates.
    seen = set(new_list)
    # Add any base that was already in filtered (keeps order Breakfast→Lunch→Dinner at the front)
    for b in base:
        if b in filtered and b not in seen:
            new_list.append(b)
            seen.add(b)
    # Add the remaining non-base items in their filtered order
    for m in filtered:
        if m not in base and m not in seen:
            new_list.append(m)
            seen.add(m)

    return new_list


#%% Transform all recipes and collect a change log
changed = []  # (name, before, after)

for r in recipes:
    name = r.get("name", "<unnamed>")
    meal_types = r.get("mealType", [])
    if not isinstance(meal_types, list):
        continue

    has_app_or_side = ("Appetizer" in meal_types) or ("Side Dish" in meal_types)
    if not has_app_or_side:
        continue

    before = list(meal_types)
    after = normalize_meal_types(meal_types)

    # Only record/save if changed
    if before != after:
        r["mealType"] = after
        changed.append((name, before, after))


#%% Save the new file
output = {"recipes": recipes}
with output_path.open("w", encoding="utf-8") as f:
    json.dump(output, f, indent=2, ensure_ascii=False)

print(f"Saved updated recipes to {output_path.resolve()}")


#%% Show a friendly change report
if not changed:
    print("No recipes required changes.")
else:
    print("Changed recipes (Name | Old -> New):")
    for name, before, after in changed:
        print(f" - {name} | {before} -> {after}")
    print(f"\nTotal changed: {len(changed)}")
