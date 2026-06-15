-- Copyright (C) 2018  Intel Corporation. All rights reserved.
-- Your use of Intel Corporation's design tools, logic functions 
-- and other software and tools, and its AMPP partner logic 
-- functions, and any output files from any of the foregoing 
-- (including device programming or simulation files), and any 
-- associated documentation or information are expressly subject 
-- to the terms and conditions of the Intel Program License 
-- Subscription Agreement, the Intel Quartus Prime License Agreement,
-- the Intel FPGA IP License Agreement, or other applicable license
-- agreement, including, without limitation, that your use is for
-- the sole purpose of programming logic devices manufactured by
-- Intel and sold by Intel or its authorized distributors.  Please
-- refer to the applicable agreement for further details.

-- VENDOR "Altera"
-- PROGRAM "Quartus Prime"
-- VERSION "Version 18.1.0 Build 625 09/12/2018 SJ Lite Edition"

-- DATE "06/15/2026 18:30:54"

-- 
-- Device: Altera EP4CE22F17C6 Package FBGA256
-- 

-- 
-- This VHDL file should be used for ModelSim-Altera (VHDL) only
-- 

LIBRARY CYCLONEIVE;
LIBRARY IEEE;
USE CYCLONEIVE.CYCLONEIVE_COMPONENTS.ALL;
USE IEEE.STD_LOGIC_1164.ALL;

ENTITY 	hard_block IS
    PORT (
	devoe : IN std_logic;
	devclrn : IN std_logic;
	devpor : IN std_logic
	);
END hard_block;

-- Design Ports Information
-- ~ALTERA_ASDO_DATA1~	=>  Location: PIN_C1,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- ~ALTERA_FLASH_nCE_nCSO~	=>  Location: PIN_D2,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- ~ALTERA_DCLK~	=>  Location: PIN_H1,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- ~ALTERA_DATA0~	=>  Location: PIN_H2,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- ~ALTERA_nCEO~	=>  Location: PIN_F16,	 I/O Standard: 2.5 V,	 Current Strength: 8mA


ARCHITECTURE structure OF hard_block IS
SIGNAL gnd : std_logic := '0';
SIGNAL vcc : std_logic := '1';
SIGNAL unknown : std_logic := 'X';
SIGNAL ww_devoe : std_logic;
SIGNAL ww_devclrn : std_logic;
SIGNAL ww_devpor : std_logic;
SIGNAL \~ALTERA_ASDO_DATA1~~padout\ : std_logic;
SIGNAL \~ALTERA_FLASH_nCE_nCSO~~padout\ : std_logic;
SIGNAL \~ALTERA_DATA0~~padout\ : std_logic;
SIGNAL \~ALTERA_ASDO_DATA1~~ibuf_o\ : std_logic;
SIGNAL \~ALTERA_FLASH_nCE_nCSO~~ibuf_o\ : std_logic;
SIGNAL \~ALTERA_DATA0~~ibuf_o\ : std_logic;

BEGIN

ww_devoe <= devoe;
ww_devclrn <= devclrn;
ww_devpor <= devpor;
END structure;


LIBRARY CYCLONEIVE;
LIBRARY IEEE;
USE CYCLONEIVE.CYCLONEIVE_COMPONENTS.ALL;
USE IEEE.STD_LOGIC_1164.ALL;

ENTITY 	lab12 IS
    PORT (
	Ain : IN std_logic_vector(3 DOWNTO 0);
	Bin : IN std_logic_vector(3 DOWNTO 0);
	F : IN std_logic_vector(2 DOWNTO 0);
	Sout : OUT std_logic_vector(3 DOWNTO 0);
	Overflow : OUT std_logic;
	Neg : OUT std_logic;
	a1 : OUT std_logic;
	b1 : OUT std_logic;
	c1 : OUT std_logic;
	d1 : OUT std_logic;
	e1 : OUT std_logic;
	f1 : OUT std_logic;
	g1 : OUT std_logic
	);
END lab12;

-- Design Ports Information
-- Sout[0]	=>  Location: PIN_A15,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Sout[1]	=>  Location: PIN_A13,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Sout[2]	=>  Location: PIN_B13,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Sout[3]	=>  Location: PIN_A11,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Overflow	=>  Location: PIN_L3,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Neg	=>  Location: PIN_B5,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- a1	=>  Location: PIN_D5,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- b1	=>  Location: PIN_A6,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- c1	=>  Location: PIN_D6,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- d1	=>  Location: PIN_C6,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- e1	=>  Location: PIN_E6,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- f1	=>  Location: PIN_D8,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- g1	=>  Location: PIN_F8,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Ain[0]	=>  Location: PIN_A7,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- F[1]	=>  Location: PIN_T8,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- F[0]	=>  Location: PIN_M1,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Bin[0]	=>  Location: PIN_F9,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Ain[3]	=>  Location: PIN_A5,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Bin[3]	=>  Location: PIN_C8,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Ain[2]	=>  Location: PIN_B6,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Bin[2]	=>  Location: PIN_E7,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Ain[1]	=>  Location: PIN_B7,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Bin[1]	=>  Location: PIN_E8,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- F[2]	=>  Location: PIN_B9,	 I/O Standard: 2.5 V,	 Current Strength: Default


ARCHITECTURE structure OF lab12 IS
SIGNAL gnd : std_logic := '0';
SIGNAL vcc : std_logic := '1';
SIGNAL unknown : std_logic := 'X';
SIGNAL devoe : std_logic := '1';
SIGNAL devclrn : std_logic := '1';
SIGNAL devpor : std_logic := '1';
SIGNAL ww_devoe : std_logic;
SIGNAL ww_devclrn : std_logic;
SIGNAL ww_devpor : std_logic;
SIGNAL ww_Ain : std_logic_vector(3 DOWNTO 0);
SIGNAL ww_Bin : std_logic_vector(3 DOWNTO 0);
SIGNAL ww_F : std_logic_vector(2 DOWNTO 0);
SIGNAL ww_Sout : std_logic_vector(3 DOWNTO 0);
SIGNAL ww_Overflow : std_logic;
SIGNAL ww_Neg : std_logic;
SIGNAL ww_a1 : std_logic;
SIGNAL ww_b1 : std_logic;
SIGNAL ww_c1 : std_logic;
SIGNAL ww_d1 : std_logic;
SIGNAL ww_e1 : std_logic;
SIGNAL ww_f1 : std_logic;
SIGNAL ww_g1 : std_logic;
SIGNAL \Sout[0]~output_o\ : std_logic;
SIGNAL \Sout[1]~output_o\ : std_logic;
SIGNAL \Sout[2]~output_o\ : std_logic;
SIGNAL \Sout[3]~output_o\ : std_logic;
SIGNAL \Overflow~output_o\ : std_logic;
SIGNAL \Neg~output_o\ : std_logic;
SIGNAL \a1~output_o\ : std_logic;
SIGNAL \b1~output_o\ : std_logic;
SIGNAL \c1~output_o\ : std_logic;
SIGNAL \d1~output_o\ : std_logic;
SIGNAL \e1~output_o\ : std_logic;
SIGNAL \f1~output_o\ : std_logic;
SIGNAL \g1~output_o\ : std_logic;
SIGNAL \F[2]~input_o\ : std_logic;
SIGNAL \Ain[3]~input_o\ : std_logic;
SIGNAL \Bin[3]~input_o\ : std_logic;
SIGNAL \F[0]~input_o\ : std_logic;
SIGNAL \F[1]~input_o\ : std_logic;
SIGNAL \U1|Mux0~0_combout\ : std_logic;
SIGNAL \U1|op1[3]~1_combout\ : std_logic;
SIGNAL \Ain[2]~input_o\ : std_logic;
SIGNAL \U1|op1[2]~2_combout\ : std_logic;
SIGNAL \Bin[2]~input_o\ : std_logic;
SIGNAL \U1|Mux1~0_combout\ : std_logic;
SIGNAL \Ain[1]~input_o\ : std_logic;
SIGNAL \U1|op1[1]~3_combout\ : std_logic;
SIGNAL \Bin[1]~input_o\ : std_logic;
SIGNAL \U1|Mux2~0_combout\ : std_logic;
SIGNAL \Ain[0]~input_o\ : std_logic;
SIGNAL \Bin[0]~input_o\ : std_logic;
SIGNAL \U1|Mux3~0_combout\ : std_logic;
SIGNAL \U1|op1[0]~0_combout\ : std_logic;
SIGNAL \U1|Add0~1_cout\ : std_logic;
SIGNAL \U1|Add0~3\ : std_logic;
SIGNAL \U1|Add0~5\ : std_logic;
SIGNAL \U1|Add0~7\ : std_logic;
SIGNAL \U1|Add0~8_combout\ : std_logic;
SIGNAL \final_ans[0]~7_combout\ : std_logic;
SIGNAL \final_ans[0]~10_combout\ : std_logic;
SIGNAL \final_ans[0]~8_combout\ : std_logic;
SIGNAL \final_ans[0]~11_combout\ : std_logic;
SIGNAL \final_ans[0]~9_combout\ : std_logic;
SIGNAL \final_ans[0]~12_combout\ : std_logic;
SIGNAL \final_ans[0]~13_combout\ : std_logic;
SIGNAL \U1|Add0~2_combout\ : std_logic;
SIGNAL \final_ans[0]~14_combout\ : std_logic;
SIGNAL \U1|Add0~4_combout\ : std_logic;
SIGNAL \U1|Mux2~1_combout\ : std_logic;
SIGNAL \final_ans[1]~15_combout\ : std_logic;
SIGNAL \final_ans[1]~16_combout\ : std_logic;
SIGNAL \final_ans[1]~17_combout\ : std_logic;
SIGNAL \U1|Add0~6_combout\ : std_logic;
SIGNAL \final_ans[2]~23_combout\ : std_logic;
SIGNAL \final_ans[2]~24_combout\ : std_logic;
SIGNAL \final_ans[2]~18_combout\ : std_logic;
SIGNAL \final_ans[2]~19_combout\ : std_logic;
SIGNAL \U1|Add2~0_combout\ : std_logic;
SIGNAL \U1|Mux0~1_combout\ : std_logic;
SIGNAL \final_ans[3]~20_combout\ : std_logic;
SIGNAL \final_ans[3]~21_combout\ : std_logic;
SIGNAL \final_ans[3]~22_combout\ : std_logic;
SIGNAL \Overflow~0_combout\ : std_logic;
SIGNAL \U2|Mux6~0_combout\ : std_logic;
SIGNAL \U2|Mux6~1_combout\ : std_logic;
SIGNAL \U2|Mux5~0_combout\ : std_logic;
SIGNAL \U2|Mux5~1_combout\ : std_logic;
SIGNAL \U2|Mux4~0_combout\ : std_logic;
SIGNAL \U2|Mux4~1_combout\ : std_logic;
SIGNAL \U2|Mux3~0_combout\ : std_logic;
SIGNAL \U2|Mux3~1_combout\ : std_logic;
SIGNAL \U2|Mux2~0_combout\ : std_logic;
SIGNAL \U2|Mux2~1_combout\ : std_logic;
SIGNAL \U2|Mux1~0_combout\ : std_logic;
SIGNAL \U2|Mux1~1_combout\ : std_logic;
SIGNAL \U2|Mux0~0_combout\ : std_logic;
SIGNAL \U2|Mux0~1_combout\ : std_logic;
SIGNAL \U2|ALT_INV_Mux1~1_combout\ : std_logic;
SIGNAL \U2|ALT_INV_Mux2~1_combout\ : std_logic;

COMPONENT hard_block
    PORT (
	devoe : IN std_logic;
	devclrn : IN std_logic;
	devpor : IN std_logic);
END COMPONENT;

BEGIN

ww_Ain <= Ain;
ww_Bin <= Bin;
ww_F <= F;
Sout <= ww_Sout;
Overflow <= ww_Overflow;
Neg <= ww_Neg;
a1 <= ww_a1;
b1 <= ww_b1;
c1 <= ww_c1;
d1 <= ww_d1;
e1 <= ww_e1;
f1 <= ww_f1;
g1 <= ww_g1;
ww_devoe <= devoe;
ww_devclrn <= devclrn;
ww_devpor <= devpor;
\U2|ALT_INV_Mux1~1_combout\ <= NOT \U2|Mux1~1_combout\;
\U2|ALT_INV_Mux2~1_combout\ <= NOT \U2|Mux2~1_combout\;
auto_generated_inst : hard_block
PORT MAP (
	devoe => ww_devoe,
	devclrn => ww_devclrn,
	devpor => ww_devpor);

-- Location: IOOBUF_X38_Y34_N16
\Sout[0]~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \final_ans[0]~14_combout\,
	devoe => ww_devoe,
	o => \Sout[0]~output_o\);

-- Location: IOOBUF_X49_Y34_N2
\Sout[1]~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \final_ans[1]~17_combout\,
	devoe => ww_devoe,
	o => \Sout[1]~output_o\);

-- Location: IOOBUF_X49_Y34_N9
\Sout[2]~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \final_ans[2]~19_combout\,
	devoe => ww_devoe,
	o => \Sout[2]~output_o\);

-- Location: IOOBUF_X40_Y34_N2
\Sout[3]~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \final_ans[3]~22_combout\,
	devoe => ww_devoe,
	o => \Sout[3]~output_o\);

-- Location: IOOBUF_X0_Y10_N23
\Overflow~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \Overflow~0_combout\,
	devoe => ww_devoe,
	o => \Overflow~output_o\);

-- Location: IOOBUF_X11_Y34_N2
\Neg~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \final_ans[0]~7_combout\,
	devoe => ww_devoe,
	o => \Neg~output_o\);

-- Location: IOOBUF_X5_Y34_N16
\a1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|Mux6~1_combout\,
	devoe => ww_devoe,
	o => \a1~output_o\);

-- Location: IOOBUF_X16_Y34_N2
\b1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|Mux5~1_combout\,
	devoe => ww_devoe,
	o => \b1~output_o\);

-- Location: IOOBUF_X9_Y34_N9
\c1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|Mux4~1_combout\,
	devoe => ww_devoe,
	o => \c1~output_o\);

-- Location: IOOBUF_X18_Y34_N23
\d1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|Mux3~1_combout\,
	devoe => ww_devoe,
	o => \d1~output_o\);

-- Location: IOOBUF_X14_Y34_N16
\e1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|ALT_INV_Mux2~1_combout\,
	devoe => ww_devoe,
	o => \e1~output_o\);

-- Location: IOOBUF_X23_Y34_N23
\f1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|ALT_INV_Mux1~1_combout\,
	devoe => ww_devoe,
	o => \f1~output_o\);

-- Location: IOOBUF_X20_Y34_N16
\g1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|Mux0~1_combout\,
	devoe => ww_devoe,
	o => \g1~output_o\);

-- Location: IOIBUF_X25_Y34_N8
\F[2]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_F(2),
	o => \F[2]~input_o\);

-- Location: IOIBUF_X14_Y34_N22
\Ain[3]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Ain(3),
	o => \Ain[3]~input_o\);

-- Location: IOIBUF_X23_Y34_N15
\Bin[3]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Bin(3),
	o => \Bin[3]~input_o\);

-- Location: IOIBUF_X0_Y16_N22
\F[0]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_F(0),
	o => \F[0]~input_o\);

-- Location: IOIBUF_X27_Y0_N15
\F[1]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_F(1),
	o => \F[1]~input_o\);

-- Location: LCCOMB_X20_Y31_N10
\U1|Mux0~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Mux0~0_combout\ = (\F[0]~input_o\ & (((!\Bin[3]~input_o\)))) # (!\F[0]~input_o\ & ((\F[1]~input_o\ & ((\Bin[3]~input_o\))) # (!\F[1]~input_o\ & (!\Ain[3]~input_o\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000110101",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \Ain[3]~input_o\,
	datab => \Bin[3]~input_o\,
	datac => \F[0]~input_o\,
	datad => \F[1]~input_o\,
	combout => \U1|Mux0~0_combout\);

-- Location: LCCOMB_X20_Y31_N0
\U1|op1[3]~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|op1[3]~1_combout\ = (\Ain[3]~input_o\ & \F[1]~input_o\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010101000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \Ain[3]~input_o\,
	datad => \F[1]~input_o\,
	combout => \U1|op1[3]~1_combout\);

-- Location: IOIBUF_X16_Y34_N8
\Ain[2]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Ain(2),
	o => \Ain[2]~input_o\);

-- Location: LCCOMB_X20_Y30_N0
\U1|op1[2]~2\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|op1[2]~2_combout\ = (\Ain[2]~input_o\ & \F[1]~input_o\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111000000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datac => \Ain[2]~input_o\,
	datad => \F[1]~input_o\,
	combout => \U1|op1[2]~2_combout\);

-- Location: IOIBUF_X16_Y34_N15
\Bin[2]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Bin(2),
	o => \Bin[2]~input_o\);

-- Location: LCCOMB_X20_Y30_N2
\U1|Mux1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Mux1~0_combout\ = (\F[0]~input_o\ & (!\Bin[2]~input_o\)) # (!\F[0]~input_o\ & ((\F[1]~input_o\ & (\Bin[2]~input_o\)) # (!\F[1]~input_o\ & ((!\Ain[2]~input_o\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0110011000100111",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[0]~input_o\,
	datab => \Bin[2]~input_o\,
	datac => \Ain[2]~input_o\,
	datad => \F[1]~input_o\,
	combout => \U1|Mux1~0_combout\);

-- Location: IOIBUF_X18_Y34_N1
\Ain[1]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Ain(1),
	o => \Ain[1]~input_o\);

-- Location: LCCOMB_X19_Y31_N4
\U1|op1[1]~3\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|op1[1]~3_combout\ = (\Ain[1]~input_o\ & \F[1]~input_o\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100000011000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \Ain[1]~input_o\,
	datac => \F[1]~input_o\,
	combout => \U1|op1[1]~3_combout\);

-- Location: IOIBUF_X20_Y34_N8
\Bin[1]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Bin(1),
	o => \Bin[1]~input_o\);

-- Location: LCCOMB_X19_Y31_N6
\U1|Mux2~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Mux2~0_combout\ = (\F[0]~input_o\ & (((!\Bin[1]~input_o\)))) # (!\F[0]~input_o\ & ((\F[1]~input_o\ & ((\Bin[1]~input_o\))) # (!\F[1]~input_o\ & (!\Ain[1]~input_o\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000101111110001",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[1]~input_o\,
	datab => \Ain[1]~input_o\,
	datac => \F[0]~input_o\,
	datad => \Bin[1]~input_o\,
	combout => \U1|Mux2~0_combout\);

-- Location: IOIBUF_X20_Y34_N22
\Ain[0]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Ain(0),
	o => \Ain[0]~input_o\);

-- Location: IOIBUF_X34_Y34_N1
\Bin[0]~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_Bin(0),
	o => \Bin[0]~input_o\);

-- Location: LCCOMB_X19_Y31_N2
\U1|Mux3~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Mux3~0_combout\ = (\F[0]~input_o\ & (((!\Bin[0]~input_o\)))) # (!\F[0]~input_o\ & ((\F[1]~input_o\ & ((\Bin[0]~input_o\))) # (!\F[1]~input_o\ & (!\Ain[0]~input_o\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000101111110001",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[1]~input_o\,
	datab => \Ain[0]~input_o\,
	datac => \F[0]~input_o\,
	datad => \Bin[0]~input_o\,
	combout => \U1|Mux3~0_combout\);

-- Location: LCCOMB_X19_Y31_N24
\U1|op1[0]~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|op1[0]~0_combout\ = (\Ain[0]~input_o\ & \F[1]~input_o\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100000011000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \Ain[0]~input_o\,
	datac => \F[1]~input_o\,
	combout => \U1|op1[0]~0_combout\);

-- Location: LCCOMB_X20_Y31_N16
\U1|Add0~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Add0~1_cout\ = CARRY((\F[0]~input_o\) # (!\F[1]~input_o\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000011011101",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[1]~input_o\,
	datab => \F[0]~input_o\,
	datad => VCC,
	cout => \U1|Add0~1_cout\);

-- Location: LCCOMB_X20_Y31_N18
\U1|Add0~2\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Add0~2_combout\ = (\U1|Mux3~0_combout\ & ((\U1|op1[0]~0_combout\ & (\U1|Add0~1_cout\ & VCC)) # (!\U1|op1[0]~0_combout\ & (!\U1|Add0~1_cout\)))) # (!\U1|Mux3~0_combout\ & ((\U1|op1[0]~0_combout\ & (!\U1|Add0~1_cout\)) # (!\U1|op1[0]~0_combout\ & 
-- ((\U1|Add0~1_cout\) # (GND)))))
-- \U1|Add0~3\ = CARRY((\U1|Mux3~0_combout\ & (!\U1|op1[0]~0_combout\ & !\U1|Add0~1_cout\)) # (!\U1|Mux3~0_combout\ & ((!\U1|Add0~1_cout\) # (!\U1|op1[0]~0_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1001011000010111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U1|Mux3~0_combout\,
	datab => \U1|op1[0]~0_combout\,
	datad => VCC,
	cin => \U1|Add0~1_cout\,
	combout => \U1|Add0~2_combout\,
	cout => \U1|Add0~3\);

-- Location: LCCOMB_X20_Y31_N20
\U1|Add0~4\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Add0~4_combout\ = ((\U1|op1[1]~3_combout\ $ (\U1|Mux2~0_combout\ $ (!\U1|Add0~3\)))) # (GND)
-- \U1|Add0~5\ = CARRY((\U1|op1[1]~3_combout\ & ((\U1|Mux2~0_combout\) # (!\U1|Add0~3\))) # (!\U1|op1[1]~3_combout\ & (\U1|Mux2~0_combout\ & !\U1|Add0~3\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0110100110001110",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U1|op1[1]~3_combout\,
	datab => \U1|Mux2~0_combout\,
	datad => VCC,
	cin => \U1|Add0~3\,
	combout => \U1|Add0~4_combout\,
	cout => \U1|Add0~5\);

-- Location: LCCOMB_X20_Y31_N22
\U1|Add0~6\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Add0~6_combout\ = (\U1|op1[2]~2_combout\ & ((\U1|Mux1~0_combout\ & (\U1|Add0~5\ & VCC)) # (!\U1|Mux1~0_combout\ & (!\U1|Add0~5\)))) # (!\U1|op1[2]~2_combout\ & ((\U1|Mux1~0_combout\ & (!\U1|Add0~5\)) # (!\U1|Mux1~0_combout\ & ((\U1|Add0~5\) # 
-- (GND)))))
-- \U1|Add0~7\ = CARRY((\U1|op1[2]~2_combout\ & (!\U1|Mux1~0_combout\ & !\U1|Add0~5\)) # (!\U1|op1[2]~2_combout\ & ((!\U1|Add0~5\) # (!\U1|Mux1~0_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1001011000010111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U1|op1[2]~2_combout\,
	datab => \U1|Mux1~0_combout\,
	datad => VCC,
	cin => \U1|Add0~5\,
	combout => \U1|Add0~6_combout\,
	cout => \U1|Add0~7\);

-- Location: LCCOMB_X20_Y31_N24
\U1|Add0~8\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Add0~8_combout\ = \U1|Mux0~0_combout\ $ (\U1|Add0~7\ $ (!\U1|op1[3]~1_combout\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101101010100101",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U1|Mux0~0_combout\,
	datad => \U1|op1[3]~1_combout\,
	cin => \U1|Add0~7\,
	combout => \U1|Add0~8_combout\);

-- Location: LCCOMB_X20_Y31_N4
\final_ans[0]~7\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~7_combout\ = (\F[2]~input_o\ & (\U1|Add0~8_combout\ & ((\U1|Mux0~0_combout\) # (\U1|op1[3]~1_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1000100010000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[2]~input_o\,
	datab => \U1|Add0~8_combout\,
	datac => \U1|Mux0~0_combout\,
	datad => \U1|op1[3]~1_combout\,
	combout => \final_ans[0]~7_combout\);

-- Location: LCCOMB_X19_Y31_N12
\final_ans[0]~10\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~10_combout\ = (\F[0]~input_o\ & (\Ain[0]~input_o\)) # (!\F[0]~input_o\ & ((\Bin[0]~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111001111000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \F[0]~input_o\,
	datac => \Ain[0]~input_o\,
	datad => \Bin[0]~input_o\,
	combout => \final_ans[0]~10_combout\);

-- Location: LCCOMB_X19_Y31_N0
\final_ans[0]~8\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~8_combout\ = (!\F[2]~input_o\ & ((\F[0]~input_o\) # (!\F[1]~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000011001111",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \F[0]~input_o\,
	datac => \F[1]~input_o\,
	datad => \F[2]~input_o\,
	combout => \final_ans[0]~8_combout\);

-- Location: LCCOMB_X19_Y31_N22
\final_ans[0]~11\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~11_combout\ = (\F[0]~input_o\ & ((\Bin[0]~input_o\))) # (!\F[0]~input_o\ & (\Ain[0]~input_o\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111110000110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \F[0]~input_o\,
	datac => \Ain[0]~input_o\,
	datad => \Bin[0]~input_o\,
	combout => \final_ans[0]~11_combout\);

-- Location: LCCOMB_X19_Y31_N10
\final_ans[0]~9\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~9_combout\ = (\F[1]~input_o\ & !\F[2]~input_o\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000011110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datac => \F[1]~input_o\,
	datad => \F[2]~input_o\,
	combout => \final_ans[0]~9_combout\);

-- Location: LCCOMB_X19_Y31_N8
\final_ans[0]~12\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~12_combout\ = (\final_ans[0]~10_combout\ & ((\final_ans[0]~11_combout\ & ((\final_ans[0]~9_combout\))) # (!\final_ans[0]~11_combout\ & (\final_ans[0]~8_combout\)))) # (!\final_ans[0]~10_combout\ & (\final_ans[0]~8_combout\ & 
-- (\final_ans[0]~11_combout\ $ (!\final_ans[0]~9_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110100000001100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~10_combout\,
	datab => \final_ans[0]~8_combout\,
	datac => \final_ans[0]~11_combout\,
	datad => \final_ans[0]~9_combout\,
	combout => \final_ans[0]~12_combout\);

-- Location: LCCOMB_X19_Y31_N26
\final_ans[0]~13\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~13_combout\ = (\final_ans[0]~8_combout\ & ((\final_ans[0]~11_combout\ & ((!\final_ans[0]~9_combout\))) # (!\final_ans[0]~11_combout\ & (!\final_ans[0]~10_combout\ & \final_ans[0]~9_combout\)))) # (!\final_ans[0]~8_combout\ & 
-- (\final_ans[0]~9_combout\ & ((!\final_ans[0]~11_combout\) # (!\final_ans[0]~10_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0001011111000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~10_combout\,
	datab => \final_ans[0]~8_combout\,
	datac => \final_ans[0]~11_combout\,
	datad => \final_ans[0]~9_combout\,
	combout => \final_ans[0]~13_combout\);

-- Location: LCCOMB_X19_Y31_N20
\final_ans[0]~14\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[0]~14_combout\ = (\final_ans[0]~7_combout\ & (((\U1|Add0~2_combout\)))) # (!\final_ans[0]~7_combout\ & ((\final_ans[0]~12_combout\ & ((\U1|Add0~2_combout\) # (!\final_ans[0]~13_combout\))) # (!\final_ans[0]~12_combout\ & 
-- (!\final_ans[0]~13_combout\ & \U1|Add0~2_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110111100000100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~7_combout\,
	datab => \final_ans[0]~12_combout\,
	datac => \final_ans[0]~13_combout\,
	datad => \U1|Add0~2_combout\,
	combout => \final_ans[0]~14_combout\);

-- Location: LCCOMB_X19_Y31_N30
\U1|Mux2~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Mux2~1_combout\ = (\F[0]~input_o\ & ((\Bin[1]~input_o\))) # (!\F[0]~input_o\ & (\Ain[1]~input_o\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111110000110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \F[0]~input_o\,
	datac => \Ain[1]~input_o\,
	datad => \Bin[1]~input_o\,
	combout => \U1|Mux2~1_combout\);

-- Location: LCCOMB_X19_Y31_N16
\final_ans[1]~15\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[1]~15_combout\ = (\final_ans[0]~8_combout\ & (((\final_ans[0]~9_combout\)) # (!\U1|Mux2~1_combout\))) # (!\final_ans[0]~8_combout\ & (((\U1|Add0~4_combout\ & !\final_ans[0]~9_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100110001110100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U1|Mux2~1_combout\,
	datab => \final_ans[0]~8_combout\,
	datac => \U1|Add0~4_combout\,
	datad => \final_ans[0]~9_combout\,
	combout => \final_ans[1]~15_combout\);

-- Location: LCCOMB_X19_Y31_N18
\final_ans[1]~16\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[1]~16_combout\ = (\final_ans[0]~9_combout\ & ((\final_ans[1]~15_combout\ & ((\Ain[1]~input_o\) # (\Bin[1]~input_o\))) # (!\final_ans[1]~15_combout\ & (\Ain[1]~input_o\ & \Bin[1]~input_o\)))) # (!\final_ans[0]~9_combout\ & 
-- (\final_ans[1]~15_combout\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110110011000100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~9_combout\,
	datab => \final_ans[1]~15_combout\,
	datac => \Ain[1]~input_o\,
	datad => \Bin[1]~input_o\,
	combout => \final_ans[1]~16_combout\);

-- Location: LCCOMB_X19_Y31_N28
\final_ans[1]~17\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[1]~17_combout\ = (\final_ans[0]~7_combout\ & (\U1|Add0~2_combout\ $ ((\U1|Add0~4_combout\)))) # (!\final_ans[0]~7_combout\ & (((\final_ans[1]~16_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0111110100101000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~7_combout\,
	datab => \U1|Add0~2_combout\,
	datac => \U1|Add0~4_combout\,
	datad => \final_ans[1]~16_combout\,
	combout => \final_ans[1]~17_combout\);

-- Location: LCCOMB_X20_Y30_N4
\final_ans[2]~23\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[2]~23_combout\ = (\Bin[2]~input_o\ & (\F[1]~input_o\ $ (((!\F[0]~input_o\ & !\Ain[2]~input_o\))))) # (!\Bin[2]~input_o\ & ((\Ain[2]~input_o\ & (\F[0]~input_o\)) # (!\Ain[2]~input_o\ & ((!\F[1]~input_o\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110100000100111",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[0]~input_o\,
	datab => \Bin[2]~input_o\,
	datac => \Ain[2]~input_o\,
	datad => \F[1]~input_o\,
	combout => \final_ans[2]~23_combout\);

-- Location: LCCOMB_X20_Y31_N14
\final_ans[2]~24\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[2]~24_combout\ = (!\F[2]~input_o\ & (\final_ans[2]~23_combout\ $ (\U1|Add0~6_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000001011010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[2]~23_combout\,
	datac => \U1|Add0~6_combout\,
	datad => \F[2]~input_o\,
	combout => \final_ans[2]~24_combout\);

-- Location: LCCOMB_X19_Y31_N14
\final_ans[2]~18\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[2]~18_combout\ = (\U1|Add0~4_combout\) # (\U1|Add0~2_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111111111110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datac => \U1|Add0~4_combout\,
	datad => \U1|Add0~2_combout\,
	combout => \final_ans[2]~18_combout\);

-- Location: LCCOMB_X20_Y31_N6
\final_ans[2]~19\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[2]~19_combout\ = \U1|Add0~6_combout\ $ (((\final_ans[0]~7_combout\ & ((\final_ans[2]~18_combout\))) # (!\final_ans[0]~7_combout\ & (\final_ans[2]~24_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101011010100110",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U1|Add0~6_combout\,
	datab => \final_ans[2]~24_combout\,
	datac => \final_ans[0]~7_combout\,
	datad => \final_ans[2]~18_combout\,
	combout => \final_ans[2]~19_combout\);

-- Location: LCCOMB_X20_Y31_N30
\U1|Add2~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Add2~0_combout\ = (\U1|Add0~2_combout\) # ((\U1|Add0~6_combout\) # (\U1|Add0~4_combout\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111111111111100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U1|Add0~2_combout\,
	datac => \U1|Add0~6_combout\,
	datad => \U1|Add0~4_combout\,
	combout => \U1|Add2~0_combout\);

-- Location: LCCOMB_X20_Y31_N8
\U1|Mux0~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U1|Mux0~1_combout\ = (\F[0]~input_o\ & (\Bin[3]~input_o\)) # (!\F[0]~input_o\ & ((\Ain[3]~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100111111000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \Bin[3]~input_o\,
	datac => \F[0]~input_o\,
	datad => \Ain[3]~input_o\,
	combout => \U1|Mux0~1_combout\);

-- Location: LCCOMB_X20_Y31_N26
\final_ans[3]~20\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[3]~20_combout\ = (\final_ans[0]~9_combout\ & (((\final_ans[0]~8_combout\)))) # (!\final_ans[0]~9_combout\ & ((\final_ans[0]~8_combout\ & (!\U1|Mux0~1_combout\)) # (!\final_ans[0]~8_combout\ & ((\U1|Add0~8_combout\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1011010110110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~9_combout\,
	datab => \U1|Mux0~1_combout\,
	datac => \final_ans[0]~8_combout\,
	datad => \U1|Add0~8_combout\,
	combout => \final_ans[3]~20_combout\);

-- Location: LCCOMB_X20_Y31_N28
\final_ans[3]~21\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[3]~21_combout\ = (\final_ans[0]~9_combout\ & ((\Bin[3]~input_o\ & ((\final_ans[3]~20_combout\) # (\Ain[3]~input_o\))) # (!\Bin[3]~input_o\ & (\final_ans[3]~20_combout\ & \Ain[3]~input_o\)))) # (!\final_ans[0]~9_combout\ & 
-- (((\final_ans[3]~20_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111100011010000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[0]~9_combout\,
	datab => \Bin[3]~input_o\,
	datac => \final_ans[3]~20_combout\,
	datad => \Ain[3]~input_o\,
	combout => \final_ans[3]~21_combout\);

-- Location: LCCOMB_X20_Y31_N2
\final_ans[3]~22\ : cycloneive_lcell_comb
-- Equation(s):
-- \final_ans[3]~22_combout\ = (\final_ans[0]~7_combout\ & (\U1|Add2~0_combout\ $ (((\U1|Add0~8_combout\))))) # (!\final_ans[0]~7_combout\ & (((\final_ans[3]~21_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101110010101100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U1|Add2~0_combout\,
	datab => \final_ans[3]~21_combout\,
	datac => \final_ans[0]~7_combout\,
	datad => \U1|Add0~8_combout\,
	combout => \final_ans[3]~22_combout\);

-- Location: LCCOMB_X20_Y31_N12
\Overflow~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \Overflow~0_combout\ = (\F[2]~input_o\ & ((\U1|Add0~8_combout\ & (!\U1|Mux0~0_combout\ & !\U1|op1[3]~1_combout\)) # (!\U1|Add0~8_combout\ & (\U1|Mux0~0_combout\ & \U1|op1[3]~1_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0010000000001000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \F[2]~input_o\,
	datab => \U1|Add0~8_combout\,
	datac => \U1|Mux0~0_combout\,
	datad => \U1|op1[3]~1_combout\,
	combout => \Overflow~0_combout\);

-- Location: LCCOMB_X19_Y33_N0
\U2|Mux6~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux6~0_combout\ = (\final_ans[1]~17_combout\ & (((\final_ans[3]~22_combout\)))) # (!\final_ans[1]~17_combout\ & (\final_ans[2]~19_combout\ $ (((\final_ans[0]~14_combout\ & !\final_ans[3]~22_combout\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110111000010100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux6~0_combout\);

-- Location: LCCOMB_X19_Y33_N26
\U2|Mux6~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux6~1_combout\ = (\Overflow~0_combout\) # (!\U2|Mux6~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111001111110011",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U2|Mux6~0_combout\,
	datac => \Overflow~0_combout\,
	combout => \U2|Mux6~1_combout\);

-- Location: LCCOMB_X19_Y33_N20
\U2|Mux5~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux5~0_combout\ = (\final_ans[2]~19_combout\ & ((\final_ans[3]~22_combout\) # (\final_ans[1]~17_combout\ $ (\final_ans[0]~14_combout\)))) # (!\final_ans[2]~19_combout\ & (\final_ans[1]~17_combout\ & ((\final_ans[3]~22_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110111001001000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux5~0_combout\);

-- Location: LCCOMB_X19_Y33_N22
\U2|Mux5~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux5~1_combout\ = (\Overflow~0_combout\) # (!\U2|Mux5~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111001111110011",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U2|Mux5~0_combout\,
	datac => \Overflow~0_combout\,
	combout => \U2|Mux5~1_combout\);

-- Location: LCCOMB_X19_Y33_N16
\U2|Mux4~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux4~0_combout\ = (\final_ans[2]~19_combout\ & (((\final_ans[3]~22_combout\)))) # (!\final_ans[2]~19_combout\ & (\final_ans[1]~17_combout\ & ((\final_ans[3]~22_combout\) # (!\final_ans[0]~14_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110111000000010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux4~0_combout\);

-- Location: LCCOMB_X19_Y33_N10
\U2|Mux4~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux4~1_combout\ = (\Overflow~0_combout\) # (!\U2|Mux4~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111001111110011",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U2|Mux4~0_combout\,
	datac => \Overflow~0_combout\,
	combout => \U2|Mux4~1_combout\);

-- Location: LCCOMB_X19_Y33_N28
\U2|Mux3~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux3~0_combout\ = (\final_ans[1]~17_combout\ & ((\final_ans[3]~22_combout\) # ((\final_ans[2]~19_combout\ & \final_ans[0]~14_combout\)))) # (!\final_ans[1]~17_combout\ & (\final_ans[2]~19_combout\ $ (((\final_ans[0]~14_combout\ & 
-- !\final_ans[3]~22_combout\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110111010010100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux3~0_combout\);

-- Location: LCCOMB_X19_Y33_N6
\U2|Mux3~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux3~1_combout\ = (\Overflow~0_combout\) # (!\U2|Mux3~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111001111110011",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U2|Mux3~0_combout\,
	datac => \Overflow~0_combout\,
	combout => \U2|Mux3~1_combout\);

-- Location: LCCOMB_X19_Y33_N24
\U2|Mux2~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux2~0_combout\ = (\final_ans[0]~14_combout\) # ((\final_ans[1]~17_combout\ & ((\final_ans[3]~22_combout\))) # (!\final_ans[1]~17_combout\ & (\final_ans[2]~19_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111111011110100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux2~0_combout\);

-- Location: LCCOMB_X19_Y33_N2
\U2|Mux2~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux2~1_combout\ = (\U2|Mux2~0_combout\) # (\Overflow~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111110011111100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U2|Mux2~0_combout\,
	datac => \Overflow~0_combout\,
	combout => \U2|Mux2~1_combout\);

-- Location: LCCOMB_X19_Y33_N12
\U2|Mux1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux1~0_combout\ = (\final_ans[2]~19_combout\ & (((\final_ans[3]~22_combout\)))) # (!\final_ans[2]~19_combout\ & ((\final_ans[1]~17_combout\) # ((\final_ans[0]~14_combout\ & !\final_ans[3]~22_combout\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1110111000110010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux1~0_combout\);

-- Location: LCCOMB_X19_Y33_N14
\U2|Mux1~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux1~1_combout\ = (\U2|Mux1~0_combout\) # (\Overflow~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111101011111010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U2|Mux1~0_combout\,
	datac => \Overflow~0_combout\,
	combout => \U2|Mux1~1_combout\);

-- Location: LCCOMB_X19_Y33_N8
\U2|Mux0~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux0~0_combout\ = (\final_ans[1]~17_combout\ & (!\final_ans[3]~22_combout\ & ((!\final_ans[0]~14_combout\) # (!\final_ans[2]~19_combout\)))) # (!\final_ans[1]~17_combout\ & (\final_ans[2]~19_combout\ $ (((\final_ans[3]~22_combout\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0001000101101110",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \final_ans[1]~17_combout\,
	datab => \final_ans[2]~19_combout\,
	datac => \final_ans[0]~14_combout\,
	datad => \final_ans[3]~22_combout\,
	combout => \U2|Mux0~0_combout\);

-- Location: LCCOMB_X19_Y33_N18
\U2|Mux0~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Mux0~1_combout\ = (\Overflow~0_combout\) # (\U2|Mux0~0_combout\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111110011111100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \Overflow~0_combout\,
	datac => \U2|Mux0~0_combout\,
	combout => \U2|Mux0~1_combout\);

ww_Sout(0) <= \Sout[0]~output_o\;

ww_Sout(1) <= \Sout[1]~output_o\;

ww_Sout(2) <= \Sout[2]~output_o\;

ww_Sout(3) <= \Sout[3]~output_o\;

ww_Overflow <= \Overflow~output_o\;

ww_Neg <= \Neg~output_o\;

ww_a1 <= \a1~output_o\;

ww_b1 <= \b1~output_o\;

ww_c1 <= \c1~output_o\;

ww_d1 <= \d1~output_o\;

ww_e1 <= \e1~output_o\;

ww_f1 <= \f1~output_o\;

ww_g1 <= \g1~output_o\;
END structure;


