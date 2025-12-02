// wwwroot/js/recipe-create.js
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('recipeForm');

    // ---------- INGREDIENTS ----------
    const ingredientInput = document.getElementById('ingredientInput');
    const addIngredientBtn = document.getElementById('addIngredientBtn');
    const ingredientList = document.getElementById('ingredientList');

    function addIngredient() {
        if (!ingredientInput || !ingredientList) return;
        const value = ingredientInput.value.trim();
        if (!value) return;

        const chip = document.createElement('span');
        chip.className = 'badge bg-light text-dark border ingredient-item';
        chip.style.cursor = 'pointer';
        chip.dataset.value = value;
        chip.textContent = value;

        chip.addEventListener('click', function () {
            chip.remove();
        });

        ingredientList.appendChild(chip);
        ingredientInput.value = '';
        ingredientInput.focus();
    }

    if (addIngredientBtn) {
        addIngredientBtn.addEventListener('click', addIngredient);
    }

    // ---------- TAGS ----------
    const tagInput = document.getElementById('tagInput');
    const addTagBtn = document.getElementById('addTagBtn');
    const tagList = document.getElementById('tagList');

    function addTag(valueFromOutside) {
        if (!tagList) return;
        const value = (valueFromOutside ?? (tagInput ? tagInput.value : '')).trim();
        if (!value) return;

        // avoid duplicate tags
        const exists = Array.from(tagList.querySelectorAll('.tag-item'))
            .some(x => x.dataset.value.toLowerCase() === value.toLowerCase());
        if (exists) {
            if (tagInput) tagInput.value = '';
            return;
        }

        const chip = document.createElement('span');
        chip.className = 'badge bg-light text-dark border tag-item';
        chip.style.cursor = 'pointer';
        chip.dataset.value = value;
        chip.textContent = value;

        chip.addEventListener('click', function () {
            chip.remove();
        });

        tagList.appendChild(chip);
        if (tagInput) {
            tagInput.value = '';
            tagInput.focus();
        }
    }

    if (addTagBtn) {
        addTagBtn.addEventListener('click', () => addTag());
    }

    // ---------- INSTRUCTIONS (steps) ----------
    const instructionInput = document.getElementById('instructionInput');
    const addInstructionBtn = document.getElementById('addInstructionBtn');
    const stepsContainer = document.getElementById('steps');

    function renumberSteps() {
        if (!stepsContainer) return;
        const stepItems = stepsContainer.querySelectorAll('.step-item');
        stepItems.forEach((step, index) => {
            const label = step.querySelector('strong');
        });
    }

    function createStep(text) {
        const wrapper = document.createElement('div');
        wrapper.className = 'd-flex align-items-start mb-2 step-item';
        wrapper.dataset.text = text;
        wrapper.draggable = true;

        const label = document.createElement('strong');
        label.textContent = ''; // filled by renumberSteps

        const stepTextSpan = document.createElement('span');
        stepTextSpan.textContent = text;
        stepTextSpan.className = 'me-2 flex-grow-1';

        const removeButton = document.createElement('button');
        removeButton.type = 'button';
        removeButton.className = 'btn btn-sm btn-outline-danger';
        removeButton.textContent = 'X';
        removeButton.addEventListener('click', function () {
            wrapper.remove();
            renumberSteps();
        });

        wrapper.appendChild(label);
        wrapper.appendChild(stepTextSpan);
        wrapper.appendChild(removeButton);

        return wrapper;
    }

    function addInstruction() {
        if (!instructionInput || !stepsContainer) return;
        const text = instructionInput.value.trim();
        if (!text) return;

        const stepElement = createStep(text);
        stepsContainer.appendChild(stepElement);
        renumberSteps();

        instructionInput.value = '';
        instructionInput.focus();
    }

    if (addInstructionBtn) {
        addInstructionBtn.addEventListener('click', addInstruction);
    }

    // Drag & drop reorder for steps
    let draggedStep = null;

    if (stepsContainer) {
        stepsContainer.addEventListener('dragstart', function (e) {
            const item = e.target.closest('.step-item');
            if (!item) return;
            draggedStep = item;
            e.dataTransfer.effectAllowed = 'move';
        });

        stepsContainer.addEventListener('dragover', function (e) {
            if (!draggedStep) return;
            e.preventDefault();

            const target = e.target.closest('.step-item');
            if (!target || target === draggedStep) return;

            const rect = target.getBoundingClientRect();
            const offset = e.clientY - rect.top;

            if (offset > rect.height / 2) {
                target.after(draggedStep);
            } else {
                target.before(draggedStep);
            }
        });

        stepsContainer.addEventListener('drop', function (e) {
            if (!draggedStep) return;
            e.preventDefault();
            draggedStep = null;
            renumberSteps();
        });

        stepsContainer.addEventListener('dragend', function () {
            draggedStep = null;
        });
    }

    // ---------- BEVERAGE TYPE (Alcoholic / Non-Alcoholic) ----------
    const mainMealTypeSelect = document.getElementById('mainMealType');
    const beverageTypeWrapper = document.getElementById('beverageTypeWrapper');

    function updateBeverageVisibility() {
        if (!mainMealTypeSelect || !beverageTypeWrapper) return;

        if (mainMealTypeSelect.value === 'Beverage') {
            beverageTypeWrapper.classList.remove('d-none');
        } else {
            beverageTypeWrapper.classList.add('d-none');
            // clear radio selection
            const radios = document.querySelectorAll('input[name="BeverageAlcohol"]');
            radios.forEach(r => { r.checked = false; });
        }
    }

    if (mainMealTypeSelect) {
        mainMealTypeSelect.addEventListener('change', updateBeverageVisibility);
        updateBeverageVisibility();
    }

    // ---------- ENTER KEY BEHAVIOR ----------
    if (form) {
        form.addEventListener('keydown', function (e) {
            if (e.key !== 'Enter') return;
            const target = e.target;

            if (!(target instanceof HTMLElement)) return;
            if (target.tagName !== 'INPUT') {
                return; 
            }

            // Always prevent form submit via Enter in inputs
            e.preventDefault();

            if (target.id === 'ingredientInput') {
                addIngredient();
            } else if (target.id === 'tagInput') {
                addTag();
            } else if (target.id === 'instructionInput') {
                addInstruction();
            }
        });
    }

    // ---------- BEFORE SUBMIT ----------
    if (form) {
        form.addEventListener('submit', function (event) {
            // If validation already blocked the submission, do nothing.
            if (event.defaultPrevented || event.validationFailed) {
                return;
            }

            // Remove old dynamic hidden inputs
            form.querySelectorAll('.dynamic-hidden').forEach(e => e.remove());

            // Ingredients[]
            if (ingredientList) {
                const ingredientChips = ingredientList.querySelectorAll('.ingredient-item');
                ingredientChips.forEach((chip, index) => {
                    const input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = 'Ingredients[' + index + ']';
                    input.value = chip.dataset.value;   // <-- plain text, NOT numbered
                    input.classList.add('dynamic-hidden');
                    form.appendChild(input);
                });
            }


            // Tags[]
            let tagIndex = 0;
            if (tagList) {
                const tagChips = tagList.querySelectorAll('.tag-item');
                tagChips.forEach((chip) => {
                    const input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = 'Tags[' + tagIndex + ']';
                    input.value = chip.dataset.value;   // <-- plain, NOT numbered
                    input.classList.add('dynamic-hidden');
                    form.appendChild(input);
                    tagIndex++;
                });
            }


            // If Beverage, add Alcoholic / Non-Alcoholic as a tag (validation ensures it's selected)
            const mainMealType = mainMealTypeSelect ? mainMealTypeSelect.value : '';
            if (mainMealType === 'Beverage') {
                const beverageSelection = document.querySelector('input[name="BeverageAlcohol"]:checked');
                if (beverageSelection) {
                    const beverageTag = beverageSelection.value; // "Alcoholic" or "Non-Alcoholic"
                    const input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = 'Tags[' + tagIndex + ']';
                    input.value = beverageTag;
                    input.classList.add('dynamic-hidden');
                    form.appendChild(input);
                    tagIndex++;
                }
            }

            // Instructions[] – include numbered text "1. Step", "2. Step", ...
            if (stepsContainer) {
                const stepItems = stepsContainer.querySelectorAll('.step-item');
                stepItems.forEach((step, index) => {
                    const text = step.dataset.text || '';
                    const numbered = (index + 1) + '. ' + text;   // <-- numbering here

                    const input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = 'Instructions[' + index + ']';
                    input.value = numbered;                      // <-- "1. Step 1"
                    input.classList.add('dynamic-hidden');
                    form.appendChild(input);
                });
            }


            // MealType["Breakfast", "Lunch"]
            if (mainMealTypeSelect && mainMealTypeSelect.value) {
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'MealType[0]';
                input.value = mainMealTypeSelect.value;  // e.g. "Breakfast", "Beverage" (no numbering)
                input.classList.add('dynamic-hidden');
                form.appendChild(input);
            }
        });
    }
});
