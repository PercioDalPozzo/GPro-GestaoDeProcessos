--------------------------------------------------------
-- Limpa a base
--
delete from processoresponsavel ;
delete from processo;
delete from responsavel;


select setval('processoresponsavel_seq',1);
select setval('processo_seq',1);
select setval('responsavel_seq',1);


CREATE OR REPLACE FUNCTION random_between(low INT ,high INT) 
   RETURNS INT AS
$$
BEGIN
   RETURN floor(random()* (high-low + 1) + low);
END;
$$ language 'plpgsql' STRICT;


CREATE OR REPLACE FUNCTION gerar_CPF() RETURNS varchar AS $$
-- ROTINA DE GERAÇÃO DE CPF SEM LOOP
-- Retorna string com CPF aletório correto.
DECLARE
vet_cpf integer [11]; --Recebe o CPF
soma integer; -- Soma utilizada para o cálculo do DV
rest integer; -- Resto da divisão
BEGIN
-- Atribuição dos valores do Vetor
vet_cpf[0] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[1] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[2] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[3] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[4] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[5] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[6] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[7] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);
vet_cpf[8] := cast(substring (CAST (random() AS VARCHAR), 3,1) as integer);

-- CÁLCULO DO PRIMEIRO NÚMERO DO DV
-- Soma dos nove primeiros multiplicados por 10, 9, 8 e assim por diante...
soma:=(vet_cpf[0]*10)+(vet_cpf[1]*9)+(vet_cpf[2]*8)+(vet_cpf[3]*7)+(vet_cpf[4]*6)+(vet_cpf[5]*5)+(vet_cpf[6]*4)+(vet_cpf[7]*3)+(vet_cpf[8]*2);
rest:=soma % 11;
if (rest = 0) or (rest = 1) THEN
vet_cpf[9]:=0;
ELSE vet_cpf[9]:=(11-rest); END IF;
-- CÁLCULO DO SEGUNDO NÚMERO DO DV
-- Soma dos nove primeiros multiplicados por 11, 10, 9 e assim por diante...
soma:= (vet_cpf[0]*11) + (vet_cpf[1]*10) + (vet_cpf[2]*9) + (vet_cpf[3]*8) + (vet_cpf[4]*7) + (vet_cpf[5]*6) + (vet_cpf[6]*5) + (vet_cpf[7]*4) + (vet_cpf[8]*3) + (vet_cpf[9]*2);
rest:=soma % 11;
if (rest = 0) or (rest = 1) THEN
vet_cpf[10] := 0;
ELSE
vet_cpf[10] := (11-rest);
END IF;
--Retorno do CPF
RETURN trim(trim(to_char(vet_cpf[0],'9')) || trim(to_char(vet_cpf[1],'9')) || trim(to_char(vet_cpf[2],'9')) || trim(to_char(vet_cpf[3],'9')) || trim(to_char(vet_cpf[4],'9')) || trim(to_char(vet_cpf[5],'9')) || trim(to_char(vet_cpf[6],'9')) || trim(to_char(vet_cpf[7],'9'))|| trim(to_char(vet_cpf[8],'9')) || trim(to_char(vet_cpf[9],'9')) || trim(to_char(vet_cpf[10],'9')));
END;
$$ LANGUAGE PLPGSQL;



--------------------------------------------------------
-- Faz inserção de 1.000 responsaveis
--
do $$
declare 
   counter integer := 1;
begin
   while counter <= 100000 loop
      
 	  insert into responsavel (nome, cpf, email) values ( 'percio' || counter , gerar_CPF(), '');
 	 
	  counter := counter + 1;
	 
   end loop;
end$$;



--------------------------------------------------------
-- Faz inserção de 1.000.000 de processos
--
do $$
declare 
   counter integer := 1;
begin
   while counter <= 1000000 loop
   
 	  insert into processo (numeroprocesso, datadistribuicao, processosegredo, pastafisica, descricao, situacao ) 
 	  values((select lpad(counter::text , 7, '0')) || '0420168190423', '03/06/2021', true, 'Pasta'|| counter, 'Minha descricao '|| counter, 0);
     
	  counter := counter + 1;
	 
   end loop;
end$$;


--------------------------------------------------------
-- Faz vinculo alestório em cada processo com 3 responsaveis
--
insert into processoresponsavel(idprocesso, idresponsavel) select id, random_between(2,40000)   from processo;
insert into processoresponsavel(idprocesso, idresponsavel) select id, random_between(40001,70000) from processo;
insert into processoresponsavel(idprocesso, idresponsavel) select id, random_between(70001,100000) from processo;

