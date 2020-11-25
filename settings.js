const settings =
{
    VERSION: process.env.VERSION                    || { "api" : "dev", "client" : "dev" },
    DATABASE_URL: process.env.DATABASE_URL          || "postgres://mkmqubdsjwslth:6bdab8dc7a89cba14b89003536347c4b2393b776b5916f5f160beb1f418f9297@ec2-54-246-89-234.eu-west-1.compute.amazonaws.com:5432/deh00d9545rhu3",
    PLAYSTORE_URL: process.env.PLAYSTORE_URL        || "/",
    PLAYSTORE_IMG: process.env.PLAYSTORE_IMG        || "/",
    INVITE_TITLE: process.env.INVITE_TITLE          || "Play now!",
    INVITE_DESC: process.env.INVITE_DESC            || "",
    PREMIUM_UNLOCK: process.env.PREMIUM_UNLOCK      || 50,
    MASTER_PASSWORD: process.env.MASTER_PASSWORD    || "master",
    PORT: process.env.PORT                          || 3000,
}

module.exports = settings;