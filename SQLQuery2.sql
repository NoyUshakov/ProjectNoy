-- 1. מחיקת הטבלה הישנה כדי שלא תחסום אותנו
DROP TABLE IF EXISTS [dbo].[Users];

-- 2. יצירת הטבלה החדשה והמתוקנת
CREATE TABLE [dbo].[Users] (
    [UserId]    INT           IDENTITY (1, 1) NOT NULL,
    [firstName] NVARCHAR (50) NOT NULL,
    [lastName]  NVARCHAR (50) NOT NULL,
    [admin]     BIT           DEFAULT ((0)) NOT NULL,
    [country]   NVARCHAR (50) NULL,
    [city]      NVARCHAR (50) NULL,      -- מעכשיו שגיאת ה-City תיפתר!
    [phone]     NVARCHAR (20) NULL,      -- מותאם בדיוק ל-user.Phone בקוד C#
    [email]     NVARCHAR (100) NOT NULL, -- מותאם בדיוק ל-user.Email בקוד C# ומונע את הקריסה
    [username]  NVARCHAR (50) NOT NULL,
    [password]  NVARCHAR (50) NOT NULL,
    [address]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([username] ASC)
);