const settings =
{
    VERSION: process.env.VERSION                || { "api" : "dev", "client" : "dev" },
    PLAYSTORE_URL: process.env.PLAYSTORE_URL    || "/",
    PLAYSTORE_IMG: process.env.PLAYSTORE_IMG    || "/",
    INVITE_TITLE: process.env.INVITE_TITLE      || "Play now!",
    INVITE_DESC: process.env.INVITE_DESC        || "",
    PREMIUM_UNLOCK: process.env.PREMIUM_UNLOCK  || 50,
}

module.exports = settings;