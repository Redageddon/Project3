// wwwroot/js/meal-validation.js

function GetElement(id) {
    return document.getElementById(id);
}

function clearMealValidation(formElement) {
    const invalidElements = formElement.querySelectorAll('.is-invalid');
    invalidElements.forEach(el => {
        el.classList.remove('is-invalid');
        el.removeAttribute('data-validation-message');
    });
}

function markInvalid(element, message) {
    if (!element) return;
    element.classList.add('is-invalid');
    element.setAttribute('data-validation-message', message);
}

/**
 * Validate the Create Meal form:
 * - Meal name required
 * - At least one recipe selected across all selects
 * - No duplicate recipes
 */
function validateMealForm(event) {
    const form = event.target;
    if (!(form instanceof HTMLFormElement)) return;

    if (event.defaultPrevented) return;

    clearMealValidation(form);

    const errors = [];

    // ---- Name (required) ----
    const nameInput = GetElement('Name');
    const nameValue = nameInput ? nameInput.value.trim() : '';

    if (!nameValue) {
        errors.push('Meal name is required.');
        markInvalid(nameInput, 'Meal name is required.');
    }

    // ---- Recipe selections ----
    const selects = Array.from(form.querySelectorAll('.meal-recipe-select'));
    const selectedEntries = [];

    selects.forEach(sel => {
        const val = sel.value;
        if (val && val !== '0') {
            const id = parseInt(val, 10);
            if (!Number.isNaN(id)) {
                selectedEntries.push({ element: sel, id });
            }
        }
    });

    if (selectedEntries.length === 0) {
        errors.push('Please select at least one recipe for this meal.');
        if (selects.length > 0) {
            markInvalid(selects[0], 'Select at least one recipe.');
        }
    }

    // ---- No duplicates across all categories ----
    const seen = new Map(); // id -> first element
    selectedEntries.forEach(({ element, id }) => {
        if (seen.has(id)) {
            errors.push('Each recipe can only be used once in the meal.');
            markInvalid(element, 'Duplicate recipe selection.');
            markInvalid(seen.get(id), 'Duplicate recipe selection.');
        } else {
            seen.set(id, element);
        }
    });

    if (errors.length > 0) {
        event.preventDefault();
        event.stopPropagation();
        event.validationFailed = true;

        const messageText = 'Please fix the following issues:\n\n' +
            errors.map(msg => '• ' + msg).join('\n');

        alert(messageText);
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('mealForm');
    if (!form) return;

    form.addEventListener('submit', validateMealForm);
});
