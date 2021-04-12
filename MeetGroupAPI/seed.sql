Create table usuario(
idUsuario SERIAL PRIMARY KEY,
nomeUsuario varchar(100) not null,
telefoneUsuario varchar(11),
emialUsuario varchar(100) not null,
senhaUsuario varchar(53) not null,
statusUsuario boolean not null
);

create table equipamento(
idEquipamento SERIAL PRIMARY KEY,
nomeEquipamento varchar(50) not null,
statusEquipamento boolean not null
);

create table sala(
idSala SERIAL PRIMARY KEY,
numeroSala int not null,
quantidadeLugaresSala int not null,
quantidadeEquipamentoSala int not null,
idSalaEquipamento int not null,
statusSala boolean not null,
FOREIGN KEY (idSalaEquipamento) REFERENCES equipamento(idEquipamento)
);

create table reserva(
idReserva SERIAL PRIMARY KEY,
dataInicioReserva Date not null,
dataFimReserva Date not null,
horaInicioReserva varchar(8) not null,
horaFimReserva varchar(8) not null,
quantidadePessoasReserva int not null,
computadorReserva boolean null,
tvReserva boolean null,
internetReserva boolean null,
webcamReserva boolean null,
idUsuarioReserva int not null,
statusReserva boolean null
);



INSERT INTO usuario(idusuario, nomeusuario, telefoneusuario, emialusuario, senhausuario, statususuario) VALUES (1,"vinicius campelo","61991759170","autanbr@gmail.com","kwZMESCCWCQ=",true)
INSERT INTO usuario(idusuario, nomeusuario, telefoneusuario, emialusuario, senhausuario, statususuario) VALUES (2,"Maria Silva","62999855652","mariasilva@email.com","FgZbdvWjq6E=",true)
INSERT INTO usuario(idusuario, nomeusuario, telefoneusuario, emialusuario, senhausuario, statususuario) VALUES (3,"Anderson Silva","6199487589","anderson@email.com","kwZMESCCWCQ=",true)

INSERT INTO equipamento(idequipamento, nomeequipamento, statusequipamento) VALUES (1,"Computador",true)
INSERT INTO equipamento(idequipamento, nomeequipamento, statusequipamento) VALUES (2,"TV",true)
INSERT INTO equipamento(idequipamento, nomeequipamento, statusequipamento) VALUES (3,"Internet",true)
INSERT INTO equipamento(idequipamento, nomeequipamento, statusequipamento) VALUES (4,"Webcam",true)
INSERT INTO equipamento(idequipamento, nomeequipamento, statusequipamento) VALUES (5,"Todos",true)
INSERT INTO equipamento(idequipamento, nomeequipamento, statusequipamento) VALUES (6,"Inexistente",true)

INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (1,1,10,4,5,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (3,3,10,4,5,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (4,4,10,4,5,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (6,6,10,1,3,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (7,7,10,1,3,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (9,9,3,4,5,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (10,10,3,4,5,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (11,11,20,0,6,true)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (2,2,10,4,5,false)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (5,5,10,4,5,false)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (8,8,3,4,5,false)
INSERT INTO sala(idsala, numerosala, quantidadelugaressala, quantidadeequipamentosala, idsalaequipamento, statussala) VALUES (12,12,20,0,6,false)

INSERT INTO reserva(idreserva, datainicioreserva, datafimreserva, horainicioreserva, horafimreserva, quantidadepessoasreserva, computadorreserva, tvreserva, internetreserva, webcamreserva, idusuarioreserva, statusreserva) VALUES (1,"2021-04-12","2021-04-13","8:00","10:01",20,true,true,true,true,0,true)