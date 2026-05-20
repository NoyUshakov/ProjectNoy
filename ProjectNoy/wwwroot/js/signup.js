// פונקציה לבדיקה של שם משתמש
function validateUsername() {
    const username = document.getElementById("username").value.trim();
    const err = document.getElementById("usernameErr");
    err.innerText = "";

    // חובה לפחות 2 תווים
    if (username.length < 2) {
        err.innerText = "שם המשתמש חייב להיות לפחות 2 תווים.";
        return false;
    }

    // חייב להתחיל באות אנגלית
    if (!/^[A-Za-z]/.test(username)) {
        err.innerText = "שם המשתמש חייב להתחיל באות אנגלית.";
        return false;
    }

    // רק אותיות, מספרים ותווים מיוחדים ללא רווחים
    if (!/^[A-Za-z0-9!@#$%^&*._-]+$/.test(username)) {
        err.innerText =
            "שם המשתמש יכול להכיל רק אותיות אנגליות, מספרים ותווים מיוחדים.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של סיסמה
function validatePassword() {
    const password = document.getElementById("password").value;
    const err = document.getElementById("passwordErr");
    err.innerText = "";

    // חובה בין 6 ל-12 תווים
    if (password.length < 6 || password.length > 12) {
        err.innerText = "הסיסמה חייבת להכיל בין 6 ל-12 תווים.";
        return false;
    }

    // רק תווים מותרים
    if (!/^[A-Za-z0-9!@#$%^&*._-]+$/.test(password)) {
        err.innerText =
            "הסיסמה יכולה להכיל רק אותיות אנגליות, מספרים ותווים מיוחדים.";
        return false;
    }

    // לפחות אות גדולה אחת
    if (!/[A-Z]/.test(password)) {
        err.innerText = "הסיסמה חייבת להכיל לפחות אות גדולה אחת.";
        return false;
    }

    // לפחות מספר אחד
    if (!/\d/.test(password)) {
        err.innerText = "הסיסמה חייבת להכיל לפחות ספרה אחת.";
        return false;
    }

    // לפחות תו מיוחד אחד
    if (!/[!@#$%^&*._-]/.test(password)) {
        err.innerText =
            "הסיסמה חייבת להכיל לפחות תו מיוחד אחד.";
        return false;
    }

    // אסור שלושה תווים זהים ברצף
    if (/(.)\1\1/.test(password)) {
        err.innerText =
            "אסור שהסיסמה תכיל שלושה תווים זהים ברצף.";
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

    // רק עברית או אנגלית ללא ערבוב
    if (!/^([A-Za-z ]+|[א-ת ]+)$/.test(firstName)) {
        err.innerText =
            "שם פרטי יכול להכיל רק אותיות אנגליות או עבריות, בלי ערבוב.";
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

    // רק עברית או אנגלית ללא ערבוב
    if (!/^([A-Za-z ]+|[א-ת ]+)$/.test(lastName)) {
        err.innerText =
            "שם משפחה יכול להכיל רק אותיות אנגליות או עבריות, בלי ערבוב.";
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

    // בדיקת גיל מינימלי 18
    const today = new Date();
    const birth = new Date(birthDate);

    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();

    if (
        monthDiff < 0 ||
        (monthDiff === 0 && today.getDate() < birth.getDate())
    ) {
        age--;
    }

    if (age < 18) {
        err.innerText = "יש להיות מעל גיל 18.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של טלפון
function validatePhone() {
    const phone = document.getElementById("phone").value.trim();
    const err = document.getElementById("phoneErr");
    err.innerText = "";

    // מקף אופציונלי
    const phoneRegex =
        /^(0[2-489]-?\d{7}|05[0-9]-?\d{7}|07[0-9]-?\d{7})$/;

    if (!phoneRegex.test(phone)) {
        err.innerText =
            "טלפון לא חוקי. חייב להיות עם קידומת חוקית.";
        return false;
    }

    return true;
}

// פונקציה לבדיקה של אימייל
function validateEmail() {
    const email = document.getElementById("email").value.trim();
    const err = document.getElementById("emailErr");
    err.innerText = "";

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email)) {
        err.innerText = "דוא״ל לא חוקי.";
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