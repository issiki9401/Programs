library ieee;
use ieee.std_logic_1164.all;

entity lab8 is
	port( A: in std_logic;
			B: in std_logic;
			C: in std_logic;
			D: in std_logic;
			b2: out std_logic;
			c2: out std_logic;
			a1: out std_logic;
			b1: out std_logic;
			c1: out std_logic;
			d1: out std_logic;
			e1: out std_logic;
			f1: out std_logic;
			g1: out std_logic;
			LED_A: out std_logic;
			LED_B: out std_logic;
			LED_C: out std_logic;
			LED_D: out std_logic);
end entity;

architecture arch of lab8 is

signal nA: std_logic;
signal nB: std_logic;
signal nC: std_logic;
signal nD: std_logic;

begin

	nA <= not A;
	nB <= not B;
	nC <= not C;
	nD <= not D;

	b2 <= (A and C) or (A and B);
	c2 <= (A and C) or (A and B);
	a1 <= ((not B) and (not D)) or (A and (not C)) or (B and D) or ((not A) and C);
	b1 <= ((not A) and (not B)) or ((not C) and (not D)) or (a and (not B)) or ((not C) and A) or ((not A) and C and D);
	c1 <= D or ((not A) and (not C)) or (A and (not B)) or (A and C) or (B and C);
	d1 <= (A and (not C)) or ((not B) and (not D)) or (B and (not C) and D) or (A and B and D) or (((not A) and (not B)) and C) or (((not A) and (not D)) and C);
	e1 <= ((not B) and (not D)) or (A and ((not C) and (not D))) or (((not A) and (not D)) and C);
	f1 <= ((not B) and (not C) and (not D)) or ((not A) and B) or (((not C) and (not B)) and A) or (B and C) or ((not D) and A and C);
	g1 <= (B and (not C)) or (A and (not C)) or (A and B) or (((not A) and (not B)) and C) or (((not A) and (not D)) and C);
	
end architecture;