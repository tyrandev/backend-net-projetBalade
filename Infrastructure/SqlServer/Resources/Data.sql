insert into users(name, email, password) 
values ('userTest1','mailUser1', 'mdpUser1'), ('userTest2','mailUser2', 'mdpUser2');

insert into dog(nameDog, raceDog, dateOfBirth, idUser)
values ('dogUser1', 'border',  '2017-11-20' , 1), ('dogUser2', 'shiba', '2015-11-20', 2);

insert into admin(nameAdmin, password)
values('admin', 'admin');

insert into ride(nameRide, place, difficulty, idUser)
values('testBalade1', 'Mons', 2, 1), ('testBalade2', 'Bruxelles', 5, 2);

insert into comment(content, score, difficulty, idUser, idRide)
values('nice', 3, 4, 1, 1);
