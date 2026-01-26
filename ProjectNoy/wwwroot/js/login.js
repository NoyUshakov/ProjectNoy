// loginValidation.js

document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");
    const passwordInput = document.getElementById("psw");
    const errorSpan = document.getElementById("passError");

    // פונקציית הבדיקה לסיסמה
    function validatePassword() {
        const val = passwordInput.value;
        let errorMessage = "";

        // 1. אורך 6-12 תווים
        if (val.length < 6 || val.length > 12) {
            errorMessage = "הסיסמה חייבת להיות בין 6 ל-12 תווים.";
        }
        // 2. בדיקת רצף של 3 תווים זהים (למשל aaa, 111, !!!)
        else if (/(.)\1\1/.test(val)) {
            errorMessage = "אסור להשתמש ב-3 תווים זהים ברצף.";
        }
        // 3. לפחות אות גדולה אחת
        else if (!/[A-Z]/.test(val)) {
            errorMessage = "חובה לכלול לפחות אות גדולה אחת.";
        }
        // 4. לפחות ספרה אחת
        else if (!/[0-9]/.test(val)) {
            errorMessage = "חובה לכלול לפחות ספרה אחת.";
        }
        // 5. לפחות תו מיוחד אחד
        else if (!/[!@#$%^&*()_+=-]/.test(val)) {
            errorMessage = "חובה לכלול לפחות תו מיוחד אחד.";
        }
        // 6. חסימת עברית ורווחים
        else if (/[א-ת\s]/.test(val)) {
            errorMessage = "אין להשתמש בעברית או ברווחים.";
        }

        // הגדרת תקינות לשדה (מונע Submit אם יש הודעה)
        passwordInput.setCustomValidity(errorMessage);
        errorSpan.textContent = errorMessage;
    }

    // הפעלת הבדיקה בכל הקלדה
    passwordInput.addEventListener("input", validatePassword);

    // איפוס הודעות שגיאה בלחיצה על Reset
    document.getElementById("resetBtn").addEventListener("click", function () {
        errorSpan.textContent = "";
        passwordInput.setCustomValidity("");
    });
});