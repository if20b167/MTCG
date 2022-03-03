 /*
 DROP TABLE tradings;
 DROP TABLE own_cards;
 DROP TABLE sessions;
 DROP TABLE decks;
 DROP TABLE cards;
 DROP TABLE users;
 */

CREATE TABLE
    IF NOT EXISTS
        users (
            u_id    char PRIMARY KEY,
            u_username    varchar NOT NULL UNIQUE,
            u_password    varchar NOT NULL,
            u_coins   integer default 20,
            u_deck varchar(126),
            u_elo integer NOT NULL
);

CREATE TABLE
    IF NOT EXISTS
        cards (
            c_id    char(36) NOT NULL CONSTRAINT cards_pk PRIMARY KEY,
            c_name varchar(50) NOT NULL,
            c_element_type  integer,
            c_card_type integer,
            c_damage    integer,
            c_in_deck   boolean default false,
            c_for_trade boolean default false,
            c_user  varchar(20) CONSTRAINT cards_users_u_id_fk REFERENCES users
);

CREATE TABLE
    IF NOT EXISTS
        decks (
            d_id    char(36) NOT NULL CONSTRAINT decks_pk PRIMARY KEY,
            d_user  varchar(20) NOT NULL CONSTRAINT decks_users_u_id_fk REFERENCES users,
            d_name  char(36) default 'deck_nameless'
);

CREATE TABLE
    IF NOT EXISTS
        sessions (
            s_token     varchar NOT NULL,
            s_user  char NOT NULL CONSTRAINT sessions_users_u_id_fk REFERENCES users on delete cascade,
            s_timestamp timestamp NOT NULL
);

CREATE TABLE
    IF NOT EXISTS
        own_cards (
            d_id    char(36) NOT NULL CONSTRAINT own_cards_decks_d_id_fk REFERENCES decks,
            c_id    char(36) NOT NULL CONSTRAINT own_cards_cards_c_id_fk REFERENCES cards on delete cascade,
            CONSTRAINT own_cards_pk UNIQUE (d_id, c_id)
);

CREATE TABLE
    IF NOT EXISTS
        tradings (
            t_id    char(36) NOT NULL CONSTRAINT tradings_pk PRIMARY KEY,
            t_user  varchar(20) NOT NULL CONSTRAINT tradings_users_u_id_fk REFERENCES users,
            t_card  char NOT NULL CONSTRAINT tradings_cards_c_id_fk REFERENCES cards
);



