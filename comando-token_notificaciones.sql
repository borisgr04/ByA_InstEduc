create table tokens_notificaciones
(
	id int primary key,
	id_tercero int  not null,
	token nvarchar not null,
	foreign key (id_tercero) references terceros(id)
)