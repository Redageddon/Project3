// wwwroot/js/recipe-validation.js

// ---------- HELPER FUNCTIONS ----------

/**
 * Get an element by id (short helper).
 * @param {string} id
 * @returns {HTMLElement|null}
 */
function GetElement(id) {
    return document.getElementById(id);
}

// Backwards-compat alias if you want to keep using getElement
function getElement(id) {
    return GetElement(id);
}

/**
 * Get the trimmed value of an input element.
 * @param {HTMLInputElement|null} inputElement
 * @returns {string}
 */
function getTrimmedInputValue(inputElement) {
    if (!inputElement) return '';
    return inputElement.value.trim();
}

/**
 * Get the value of a checked radio in a group.
 * @param {string} groupName
 * @returns {string}
 */
function getRadioGroupValue(groupName) {
    const checkedRadio = document.querySelector(`input[name="${groupName}"]:checked`);
    return checkedRadio ? checkedRadio.value : '';
}

/**
 * Mark a single field as invalid, for visual feedback.
 * @param {HTMLElement|null} element
 * @param {string} message
 */
function markFieldInvalid(element, message) {
    if (!element) return;
    element.classList.add('is-invalid');
    element.setAttribute('data-validation-message', message);
}

/**
 * Remove invalid state from a field.
 * @param {HTMLElement|null} element
 */
function clearFieldInvalid(element) {
    if (!element) return;
    element.classList.remove('is-invalid');
    element.removeAttribute('data-validation-message');
}

/**
 * Clear all validation state inside the form.
 * @param {HTMLFormElement} formElement
 */
function clearFormValidationState(formElement) {
    const invalidElements = formElement.querySelectorAll('.is-invalid');
    invalidElements.forEach(el => {
        el.classList.remove('is-invalid');
        el.removeAttribute('data-validation-message');
    });
}

/**
 * Try to parse a positive integer from an input.
 * Returns NaN if not valid.
 * @param {HTMLInputElement|null} inputElement
 * @returns {number}
 */
function getPositiveIntegerValue(inputElement) {
    if (!inputElement) return NaN;
    const trimmed = inputElement.value.trim();
    if (trimmed === '') return NaN;
    const numberValue = parseInt(trimmed, 10);
    if (isNaN(numberValue) || numberValue <= 0) return NaN;
    return numberValue;
}

// ---------- MAIN VALIDATION ----------

/**
 * Validate the recipe form on submit.
 * If there are errors, prevent submission and show an alert.
 * @param {SubmitEvent} event
 */
function validateRecipeForm(event) {
    const recipeFormElement = event.target;
    if (!(recipeFormElement instanceof HTMLFormElement)) return;

    // If some earlier listener already blocked submission, do nothing.
    if (event.defaultPrevented) return;

    clearFormValidationState(recipeFormElement);

    const errorMessages = [];

    // Important elements
    const recipeNameInput = GetElement('Name');
    const cuisineInput = GetElement('Cuisine');
    const imageUrlInput = GetElement('imageUrlInput');
    const mainMealTypeSelect = GetElement('mainMealType');
    const ingredientListContainer = GetElement('ingredientList');
    const stepsContainer = GetElement('steps');
    const prepTimeInput = GetElement('PrepTimeMinutes');
    const cookTimeInput = GetElement('CookTimeMinutes');
    const servingsInput = GetElement('Servings');
    const caloriesInput = GetElement('CaloriesPerServing');

    // ----- Recipe Name (required) -----
    const recipeName = getTrimmedInputValue(recipeNameInput);
    if (!recipeName) {
        errorMessages.push('Recipe name is required.');
        markFieldInvalid(recipeNameInput, 'Recipe name is required.');
    }

    // ----- Cuisine (required) -----
    const cuisineValue = getTrimmedInputValue(cuisineInput);
    if (!cuisineValue) {
        errorMessages.push('Cuisine is required.');
        markFieldInvalid(cuisineInput, 'Cuisine is required.');
    }

    // ----- Image URL (required + basic URL check) -----
    const imageValue = getTrimmedInputValue(imageUrlInput);
    if (!imageValue) {
        errorMessages.push('Image URL is required.');
        markFieldInvalid(imageUrlInput, 'Image URL is required.');
    } else {
        const looksLikeUrl = /^https?:\/\/.+/i.test(imageValue);
        if (!looksLikeUrl) {
            errorMessages.push('Image URL must start with http:// or https://');
            markFieldInvalid(imageUrlInput, 'Enter a valid URL.');
        }
    }

    // ----- Main Meal Type (required) -----
    const mainMealTypeValue = mainMealTypeSelect ? mainMealTypeSelect.value : '';
    if (!mainMealTypeValue) {
        errorMessages.push('Main meal type is required.');
        markFieldInvalid(mainMealTypeSelect, 'Main meal type is required.');
    }

    // ----- Beverage Alcoholic/Non-Alcoholic (when Beverage selected) -----
    if (mainMealTypeValue === 'Beverage') {
        const beverageTypeValue = getRadioGroupValue('BeverageAlcohol');
        if (!beverageTypeValue) {
            errorMessages.push('For beverages, please choose Alcoholic or Non-Alcoholic.');
            const beverageRadio = document.querySelector('input[name="BeverageAlcohol"]');
            markFieldInvalid(beverageRadio, 'Please select Alcoholic or Non-Alcoholic.');
        }
    }

    // ----- Ingredients (need at least 1 chip) -----
    if (ingredientListContainer) {
        const ingredientItems = ingredientListContainer.querySelectorAll('.ingredient-item');
        if (ingredientItems.length === 0) {
            errorMessages.push('Please add at least one ingredient.');
            const ingredientInputElement = GetElement('ingredientInput');
            markFieldInvalid(ingredientInputElement, 'Add at least one ingredient.');
        }
    }

    // ----- Instructions (need at least 1 step) -----
    if (stepsContainer) {
        const stepItems = stepsContainer.querySelectorAll('.step-item');
        if (stepItems.length === 0) {
            errorMessages.push('Please add at least one instruction step.');
            const instructionInputElement = GetElement('instructionInput');
            markFieldInvalid(instructionInputElement, 'Add at least one step.');
        }
    }

    // ----- Difficulty (radio group, required) -----
    const difficultyValue = getRadioGroupValue('Difficulty');
    if (!difficultyValue) {
        errorMessages.push('Please select a difficulty (Easy, Medium, or Hard).');
        const firstDifficultyRadio = document.querySelector('input[name="Difficulty"]');
        markFieldInvalid(firstDifficultyRadio, 'Difficulty is required.');
    }

    // ----- Prep Time (optional but must be > 0 if provided) -----
    const prepTime = getPositiveIntegerValue(prepTimeInput);
    if (prepTimeInput && prepTimeInput.value.trim() !== '' && isNaN(prepTime)) {
        errorMessages.push('Prep time must be a positive number of minutes.');
        markFieldInvalid(prepTimeInput, 'Enter a positive number.');
    }

    // ----- Cook Time (optional but must be > 0 if provided) -----
    const cookTime = getPositiveIntegerValue(cookTimeInput);
    if (cookTimeInput && cookTimeInput.value.trim() !== '' && isNaN(cookTime)) {
        errorMessages.push('Cook time must be a positive number of minutes.');
        markFieldInvalid(cookTimeInput, 'Enter a positive number.');
    }

    // ----- Servings (required, > 0) -----
    const servingsValue = getPositiveIntegerValue(servingsInput);
    if (isNaN(servingsValue)) {
        errorMessages.push('Servings is required and must be a positive number.');
        markFieldInvalid(servingsInput, 'Enter a positive number for servings.');
    }

    // ----- Calories per serving (optional but must be > 0 if provided) -----
    const caloriesValue = getPositiveIntegerValue(caloriesInput);
    if (caloriesInput && caloriesInput.value.trim() !== '' && isNaN(caloriesValue)) {
        errorMessages.push('Calories per serving must be a positive number.');
        markFieldInvalid(caloriesInput, 'Enter a positive number.');
    }

    // ----- Final decision -----
    if (errorMessages.length > 0) {
        event.preventDefault();
        event.stopPropagation();
        // Flag so other handlers (like recipe-create.js) can detect failure
        event.validationFailed = true;

        const messageText = 'Please fix the following issues:\n\n' +
            errorMessages.map(msg => '• ' + msg).join('\n');

        alert(messageText);
    }
}

// Attach validation when the DOM is ready
document.addEventListener('DOMContentLoaded', function () {
    const recipeFormElement = GetElement('recipeForm');
    if (!recipeFormElement) return;

    // Use capture phase so validation runs before other submit listeners
    recipeFormElement.addEventListener('submit', validateRecipeForm, true);
});
