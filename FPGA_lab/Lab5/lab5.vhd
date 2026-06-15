library ieee;
use ieee.std_logic_1164.all;

entity lab5 is
	port( A: in std_logic;
			B: in std_logic;
			C: in std_logic;
			F: out std_logic);
end entity;

architecture arch of lab5 is

signal notC: std_logic;
signal x1:std_logic;
signal x2:std_logic;

begin

	notC <= not C;
	x1 <= notC nand A;
	x2 <= B nand C;
	F <= x1 nand x2;
	
end architecture;