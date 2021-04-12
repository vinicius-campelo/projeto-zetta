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