library ieee;
use ieee.std_logic_1164.all;

entity seven_segment_display is
	port( S    : in  std_logic_vector(3 downto 0);
			Cout : in  std_logic;
			b2   : out std_logic;
			c2   : out std_logic;
			a1	  : out std_logic;
			b1   : out std_logic;
			c1   : out std_logic;
			d1   : out std_logic;
			e1   : out std_logic;
			f1   : out std_logic;
			g1   : out std_logic);
end entity;

architecture arch of seven_segment_display is

signal A : std_logic;
signal B : std_logic;
signal C : std_logic;
signal D : std_logic;
signal nA: std_logic;
signal nB: std_logic;
signal nC: std_logic;
signal nD: std_logic;

begin

	A  <= S(3);
	B  <= S(2);
	C  <= S(1);
	D  <= S(0);
	nA <= not S(3);
	nB <= not S(2);
	nC <= not S(1);
	nD <= not S(0);
	
	b2 <= (not Cout) and ((A and B) or (A and C));
	c2 <= (not Cout) and ((A and B) or (A and C));
	a1 <= (not Cout) and ((nB and nD) or (A and nC) or (B and D) or (nA and C));
	b1 <= (not Cout) and (nB or (A and nD) or (A and nC) or (nA and nC and nD) or (nA and C and D));
	c1 <= (not Cout) and (D or (A and nB) or (B and C) or (nA and nC));
	d1 <= (not Cout) and ((nB and nD) or (A and nC) or (A and B and D) or (B and nC and D) or (nA and nB and C) or (nA and C and nD));
	e1 <= (not Cout) and ((nB and nD) or (A and nC and nD) or (nA and C and nD));
	f1 <= (not Cout) and ((nA and nC and nD) or (nA and B and nC) or (A and nB and nC) or (A and B and C) or (B and C and nD) or (A and C and nD));
	g1 <= (not Cout) and ((A and B) or (A and nC) or (B and nC) or (nA and nB and C) or (nA and C and nD));
	
end architecture;