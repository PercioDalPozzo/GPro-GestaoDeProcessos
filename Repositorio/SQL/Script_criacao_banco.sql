create sequence responsavel_seq;
create table responsavel
(
	id int not null DEFAULT nextval('responsavel_seq'),
	nome varchar(150) not null,
	cpf varchar(11) not null,
	email varchar(400) not null,
	foto bytea, 
	constraint pk_responsavel primary key (id)
);
ALTER SEQUENCE responsavel_seq OWNED BY responsavel.id;



create sequence processo_seq;
create table processo
(
	id int not null default nextval('processo_seq'),
	numeroprocesso varchar(20) not null,
	datadistribuicao date,
	processosegredo bool not null default false,
	pastafisica varchar(50),	
	descricao varchar(1000) not null,
	situacao int not null default 0,
	idprocessopai int,	
	constraint pk_processo primary key (id),	
	constraint fk_processo_processopai foreign key (idprocessopai) references processo(id)
);
ALTER SEQUENCE processo_seq OWNED BY processo.id;
create index idx_processo_processopai on processo(idprocessopai);


create sequence processoresponsavel_seq;
create table processoresponsavel 
(
	id int not null default nextval('processoresponsavel_seq'),
	idprocesso int not null,
	idresponsavel int not null,
	constraint pk_processoresponsavel primary key (id),
	constraint fk_processoresponsavel_processo foreign key (idprocesso) references processo(id),
	constraint fk_processoresponsavel_responsavel foreign key (idresponsavel) references responsavel(id)
);
ALTER SEQUENCE processoresponsavel_seq OWNED BY processoresponsavel.id;
create index idx_processoresponsavel_processo on processoresponsavel(idprocesso);
create index idx_processoresponsavel_responsavel on processoresponsavel(idresponsavel);

