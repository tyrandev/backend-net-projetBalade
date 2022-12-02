

use dbBalade;

create table users
(
id int identity primary key,
name varchar(255) not null,
email varchar(255) not null,
password varchar(255) not null
);

create table dog
(
id int identity primary key,
nameDog varchar(255) not null,
raceDog varchar(255),
dateOfBirth date,
idUser int foreign key references users(id)
);

create table admin
(
nameAdmin varchar(255) not null,
password varchar(255) not null
);
create table ride
(
    id int identity primary key,
    nameRide varchar(255) not null,
    place varchar(255) not null,
    description varchar(1000),
    website varchar(255),
    difficulty tinyint not null,
    schedule varchar(255),
    score tinyint,
    double precision latitude,
    double precision longitude,
    idUser int foreign key references users(id)
);

create table comment
(
id int identity primary key,
content varchar(1000) not null,
score tinyint,
difficulty tinyint not null,
idUser int foreign key references users(id),
idRide int foreign key references ride(id)
);

create table message
(
id int identity primary key,
content varchar(1000) not null,
idRecipient int foreign key references users(id),
idSender int foreign key references users(id),
object varchar(255) not null
);



