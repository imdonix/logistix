CREATE TABLE resoults
(
    mapid integer NOT NULL,
    iswin boolean NOT NULL,
    score integer NOT NULL,
    lostboxes integer NOT NULL,
    "time" integer NOT NULL,
    serial bigint NOT NULL DEFAULT nextval('resoults_serial_seq'::regclass),
    email text COLLATE pg_catalog."default" NOT NULL,
    usedmultiplies boolean NOT NULL,
    CONSTRAINT resoults_pkey PRIMARY KEY (serial)
);

CREATE TABLE invites
(
    inviter text COLLATE pg_catalog."default" NOT NULL,
    iphash text COLLATE pg_catalog."default" NOT NULL,
    date date
);

CREATE TABLE users
(
    completed integer[],
    email text COLLATE pg_catalog."default" NOT NULL,
    iron integer DEFAULT 0,
    wood integer DEFAULT 0,
    premium boolean DEFAULT false,
    name text COLLATE pg_catalog."default",
    CONSTRAINT users_pkey PRIMARY KEY (email)
);

CREATE TABLE public.levels
(
    version bigint NOT NULL DEFAULT nextval('levels_id'),
    map json,
    CONSTRAINT levels_pkey PRIMARY KEY (version)
)
