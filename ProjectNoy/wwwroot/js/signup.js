// פונקציה לבדיקה של שם משתמש
function validateUsername() {
    const username = document.getElementById("username").value.trim();
    const err = document.getElementById("usernameErr");
    err.innerText = "";

    // תנאי 1: חובה לפחות 2 תווים
    if (username.length < 2) {
        err.innerText = "שם המשתמש חייב להיות לפחות 2 תווים.";
        return false;
    }

    // תנאי 2: חובה להתחיל באות אנגלית
    if (!/^[A-Za-z]/.test(username)) {
        err.innerText = "שם המשתמש חייב להתחיל באות אנגלית.";
        return false;
    }

    // תנאי 3: כולל רק אותיות אנגליות, מספרים ותווים מיוחדים, אין רווחים
    if (!/^[A-Za-z0-9!@#$%^&*]+$/.test(username)) {
        err.innerText = "שם המשתמש יכול להכיל רק אותיות אנגליות, מספרים ותווים מיוחדים (ללא רווחים).";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של סיסמה
function validatePassword() {
    const password = document.getElementById("password").value;
    const err = document.getElementById("passwordErr");
    err.innerText = "";

    // תנאי 1: חובה 6-12 תווים
    if (password.length < 6 || password.length > 12) {
        err.innerText = "הסיסמה חייבת להכיל בין 6 ל-12 תווים.";
        return false;
    }

    // תנאי 2: רק אותיות אנגליות, מספרים ותווים מיוחדים
    if (!/^[A-Za-z0-9!@#$%^&*]+$/.test(password)) {
        err.innerText = "הסיסמה יכולה להכיל רק אותיות אנגליות, מספרים ותווים מיוחדים.";
        return false;
    }

    // תנאי 3: לפחות אות גדולה אחת
    if (!/[A-Z]/.test(password)) {
        err.innerText = "הסיסמה חייבת להכיל לפחות אות גדולה אחת.";
        return false;
    }

    // תנאי 4: לפחות ספרה אחת
    if (!/\d/.test(password)) {
        err.innerText = "הסיסמה חייבת להכיל לפחות ספרה אחת.";
        return false;
    }

    // תנאי 5: לפחות תו מיוחד אחד
    if (!/[!@#$%^&*]/.test(password)) {
        err.innerText = "הסיסמה חייבת להכיל לפחות תו מיוחד אחד (!@#$%^&*).";
        return false;
    }

    // תנאי 6: אסור שלושה תווים זהים ברצף
    if (/(.)\1\1/.test(password)) {
        err.innerText = "אסור שהסיסמה תכיל שלושה תווים זהים ברצף.";
        return false;
    }

    return true;
}

// פונקציה לאימות סיסמה
function validateConfirmPassword() {
    const password = document.getElementById("password").value;
    const confirm = document.getElementById("confirmPassword").value;
    const err = document.getElementById("confirmPasswordErr");
    err.innerText = "";

    if (confirm === "") {
        err.innerText = "אנא אשר את הסיסמה.";
        return false;
    }

    if (password !== confirm) {
        err.innerText = "הסיסמאות אינן זהות.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של שם פרטי
function validateFirstName() {
    const firstName = document.getElementById("firstName").value.trim();
    const err = document.getElementById("firstNameErr");
    err.innerText = "";

    if (firstName.length < 2) {
        err.innerText = "שם פרטי חייב להיות לפחות 2 תווים.";
        return false;
    }

    // רק אותיות אנגליות או עבריות (ללא ערבוב)
    if (!/^([A-Za-z ]+|[א-ת ]+)$/.test(firstName)) {
        err.innerText = "שם פרטי יכול להכיל רק אותיות אנגליות או עבריות, בלי ערבוב.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של שם משפחה
function validateLastName() {
    const lastName = document.getElementById("lastName").value.trim();
    const err = document.getElementById("lastNameErr");
    err.innerText = "";

    if (lastName.length < 2) {
        err.innerText = "שם משפחה חייב להיות לפחות 2 תווים.";
        return false;
    }

    if (!/^([A-Za-z ]+|[א-ת ]+)$/.test(lastName)) {
        err.innerText = "שם משפחה יכול להכיל רק אותיות אנגליות או עבריות, בלי ערבוב.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של תאריך לידה
function validateBirthDate() {
    const birthDate = document.getElementById("birthDate").value;
    const err = document.getElementById("birthDateErr");
    err.innerText = "";

    if (birthDate === "") {
        err.innerText = "יש לבחור תאריך לידה.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של טלפון
function validatePhone() {
    const phone = document.getElementById("phone").value.trim();
    const err = document.getElementById("phoneErr");
    err.innerText = "";

    const phoneRegex = /^(0[2-489]-\d{7}|05[0-9]-\d{7}|07[0-9]-\d{7})$/;

    if (!phoneRegex.test(phone)) {
        err.innerText = "טלפון לא חוקי. חייב להיות עם מקף (-) וקידומת חוקית.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של אימייל
function validateEmail() {
    const email = document.getElementById("email").value.trim();
    const err = document.getElementById("emailErr");
    err.innerText = "";

    const emailRegex = /^[A-Za-z][A-Za-z0-9_-]*@[A-Za-z][A-Za-z0-9_-]*\.[0-9]{2,3}$/;

    if (!emailRegex.test(email)) {
        err.innerText = "דוא״ל לא חוקי. חייב להתחיל באות, להכיל @ ו-., וסיומת 2-3 ספרות.";
        return false;
    }

    return true;
}

// פונקציה ראשית לבדיקה כללית לפני שליחת הטופס
function validateRegister() {
    let isValid = true;

    if (!validateUsername()) isValid = false;
    if (!validatePassword()) isValid = false;
    if (!validateConfirmPassword()) isValid = false;
    if (!validateFirstName()) isValid = false;
    if (!validateLastName()) isValid = false;
    if (!validateBirthDate()) isValid = false;
    if (!validatePhone()) isValid = false;
    if (!validateEmail()) isValid = false;

    return isValid;
}
