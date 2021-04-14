CREATE SEQUENCE levels_id INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1;
CREATE SEQUENCE public.resoults_serial_seq INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1;

CREATE TABLE resoults
(
    mapid integer NOT NULL,
    iswin boolean NOT NULL,
    score integer NOT NULL,
    lostboxes integer NOT NULL,
    "time" integer NOT NULL,
    serial bigint NOT NULL DEFAULT nextval('resoults_serial_seq'),
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

CREATE TABLE levels
(
    version bigint NOT NULL DEFAULT nextval('levels_id'),
    map json,
    CONSTRAINT levels_pkey PRIMARY KEY (version)
)

CREATE TABLE bugs
(
    id bigint NOT NULL DEFAULT nextval('bug_id'::regclass),
    bug text COLLATE pg_catalog."default" NOT NULL,
    player text COLLATE pg_catalog."default",
    device text COLLATE pg_catalog."default",
    ram text COLLATE pg_catalog."default",
    fixed boolean DEFAULT false,
    date date NOT NULL DEFAULT now(),
    CONSTRAINT bugs_pkey PRIMARY KEY (id)
)