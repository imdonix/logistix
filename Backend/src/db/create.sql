CREATE TABLE result
(
    mapid integer NOT NULL,
    iswin boolean NOT NULL,
    score integer NOT NULL,
    lostboxes integer NOT NULL,
    submitted integer NOT NULL,
    email text NOT NULL,
    usedmultiplies boolean NOT NULL,
);

CREATE TABLE invites
(
    inviter text NOT NULL,
    iphash text NOT NULL,
    date date
);

CREATE TABLE users
(
    completed text,
    email text PRIMARY KEY,
    name text,
    iron integer DEFAULT 0,
    wood integer DEFAULT 0,
    premium boolean DEFAULT false,
);

CREATE TABLE levels
(
    version bigint NOT NULL,
    map json,
)

CREATE TABLE bugs
(
    id bigint NOT NULL DEFAULT ,
    bug text,
    player text,
    device text,
    ram text,
    fixed boolean DEFAULT false,
    date date NOT NULL DEFAULT now(),
)