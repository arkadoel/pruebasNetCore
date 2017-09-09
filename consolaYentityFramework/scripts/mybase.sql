
CREATE TABLE "persona"
(
    id serial NOT NULL,
    name varchar(120) NOT NULL,
    description varchar(50),
    PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
);
ALTER TABLE "persona"
    OWNER to postgres;

CREATE TABLE "telefono"
(
    idTelefono serial NOT NULL,
    idPersona serial not null,
    phoneNumber varchar(11),
    PRIMARY KEY (idTelefono)
)
WITH (
    OIDS = FALSE
);
ALTER TABLE "telefono"
    OWNER to postgres;