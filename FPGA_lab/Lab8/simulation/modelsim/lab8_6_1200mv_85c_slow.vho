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

-- DATE "04/13/2026 19:04:24"

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

ENTITY 	lab8 IS
    PORT (
	A : IN std_logic;
	B : IN std_logic;
	C : IN std_logic;
	D : IN std_logic;
	b2 : OUT std_logic;
	c2 : OUT std_logic;
	a1 : OUT std_logic;
	b1 : OUT std_logic;
	c1 : OUT std_logic;
	d1 : OUT std_logic;
	e1 : OUT std_logic;
	f1 : OUT std_logic;
	g1 : OUT std_logic;
	LED_A : OUT std_logic;
	LED_B : OUT std_logic;
	LED_C : OUT std_logic;
	LED_D : OUT std_logic
	);
END lab8;

-- Design Ports Information
-- b2	=>  Location: PIN_B3,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- c2	=>  Location: PIN_A4,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- a1	=>  Location: PIN_D3,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- b1	=>  Location: PIN_C3,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- c1	=>  Location: PIN_A3,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- d1	=>  Location: PIN_B4,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- e1	=>  Location: PIN_B5,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- f1	=>  Location: PIN_D5,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- g1	=>  Location: PIN_A6,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- LED_A	=>  Location: PIN_D12,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- LED_B	=>  Location: PIN_T11,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- LED_C	=>  Location: PIN_A12,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- LED_D	=>  Location: PIN_R5,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- A	=>  Location: PIN_M15,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- B	=>  Location: PIN_B9,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- C	=>  Location: PIN_T8,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- D	=>  Location: PIN_M1,	 I/O Standard: 2.5 V,	 Current Strength: Default


ARCHITECTURE structure OF lab8 IS
SIGNAL gnd : std_logic := '0';
SIGNAL vcc : std_logic := '1';
SIGNAL unknown : std_logic := 'X';
SIGNAL devoe : std_logic := '1';
SIGNAL devclrn : std_logic := '1';
SIGNAL devpor : std_logic := '1';
SIGNAL ww_devoe : std_logic;
SIGNAL ww_devclrn : std_logic;
SIGNAL ww_devpor : std_logic;
SIGNAL ww_A : std_logic;
SIGNAL ww_B : std_logic;
SIGNAL ww_C : std_logic;
SIGNAL ww_D : std_logic;
SIGNAL ww_b2 : std_logic;
SIGNAL ww_c2 : std_logic;
SIGNAL ww_a1 : std_logic;
SIGNAL ww_b1 : std_logic;
SIGNAL ww_c1 : std_logic;
SIGNAL ww_d1 : std_logic;
SIGNAL ww_e1 : std_logic;
SIGNAL ww_f1 : std_logic;
SIGNAL ww_g1 : std_logic;
SIGNAL ww_LED_A : std_logic;
SIGNAL ww_LED_B : std_logic;
SIGNAL ww_LED_C : std_logic;
SIGNAL ww_LED_D : std_logic;
SIGNAL \b2~output_o\ : std_logic;
SIGNAL \c2~output_o\ : std_logic;
SIGNAL \a1~output_o\ : std_logic;
SIGNAL \b1~output_o\ : std_logic;
SIGNAL \c1~output_o\ : std_logic;
SIGNAL \d1~output_o\ : std_logic;
SIGNAL \e1~output_o\ : std_logic;
SIGNAL \f1~output_o\ : std_logic;
SIGNAL \g1~output_o\ : std_logic;
SIGNAL \LED_A~output_o\ : std_logic;
SIGNAL \LED_B~output_o\ : std_logic;
SIGNAL \LED_C~output_o\ : std_logic;
SIGNAL \LED_D~output_o\ : std_logic;
SIGNAL \B~input_o\ : std_logic;
SIGNAL \C~input_o\ : std_logic;
SIGNAL \A~input_o\ : std_logic;
SIGNAL \b2~0_combout\ : std_logic;
SIGNAL \D~input_o\ : std_logic;
SIGNAL \a1~0_combout\ : std_logic;
SIGNAL \b1~0_combout\ : std_logic;
SIGNAL \c1~0_combout\ : std_logic;
SIGNAL \d1~0_combout\ : std_logic;
SIGNAL \e1~0_combout\ : std_logic;
SIGNAL \f1~0_combout\ : std_logic;
SIGNAL \g1~0_combout\ : std_logic;

COMPONENT hard_block
    PORT (
	devoe : IN std_logic;
	devclrn : IN std_logic;
	devpor : IN std_logic);
END COMPONENT;

BEGIN

ww_A <= A;
ww_B <= B;
ww_C <= C;
ww_D <= D;
b2 <= ww_b2;
c2 <= ww_c2;
a1 <= ww_a1;
b1 <= ww_b1;
c1 <= ww_c1;
d1 <= ww_d1;
e1 <= ww_e1;
f1 <= ww_f1;
g1 <= ww_g1;
LED_A <= ww_LED_A;
LED_B <= ww_LED_B;
LED_C <= ww_LED_C;
LED_D <= ww_LED_D;
ww_devoe <= devoe;
ww_devclrn <= devclrn;
ww_devpor <= devpor;
auto_generated_inst : hard_block
PORT MAP (
	devoe => ww_devoe,
	devclrn => ww_devclrn,
	devpor => ww_devpor);

-- Location: IOOBUF_X3_Y34_N2
\b2~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \b2~0_combout\,
	devoe => ww_devoe,
	o => \b2~output_o\);

-- Location: IOOBUF_X9_Y34_N23
\c2~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \b2~0_combout\,
	devoe => ww_devoe,
	o => \c2~output_o\);

-- Location: IOOBUF_X1_Y34_N9
\a1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \a1~0_combout\,
	devoe => ww_devoe,
	o => \a1~output_o\);

-- Location: IOOBUF_X1_Y34_N2
\b1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \b1~0_combout\,
	devoe => ww_devoe,
	o => \b1~output_o\);

-- Location: IOOBUF_X7_Y34_N16
\c1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \c1~0_combout\,
	devoe => ww_devoe,
	o => \c1~output_o\);

-- Location: IOOBUF_X7_Y34_N2
\d1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \d1~0_combout\,
	devoe => ww_devoe,
	o => \d1~output_o\);

-- Location: IOOBUF_X11_Y34_N2
\e1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \e1~0_combout\,
	devoe => ww_devoe,
	o => \e1~output_o\);

-- Location: IOOBUF_X5_Y34_N16
\f1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \f1~0_combout\,
	devoe => ww_devoe,
	o => \f1~output_o\);

-- Location: IOOBUF_X16_Y34_N2
\g1~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \g1~0_combout\,
	devoe => ww_devoe,
	o => \g1~output_o\);

-- Location: IOOBUF_X51_Y34_N23
\LED_A~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => GND,
	devoe => ww_devoe,
	o => \LED_A~output_o\);

-- Location: IOOBUF_X36_Y0_N23
\LED_B~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => GND,
	devoe => ww_devoe,
	o => \LED_B~output_o\);

-- Location: IOOBUF_X43_Y34_N16
\LED_C~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => GND,
	devoe => ww_devoe,
	o => \LED_C~output_o\);

-- Location: IOOBUF_X14_Y0_N23
\LED_D~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => GND,
	devoe => ww_devoe,
	o => \LED_D~output_o\);

-- Location: IOIBUF_X25_Y34_N8
\B~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_B,
	o => \B~input_o\);

-- Location: IOIBUF_X27_Y0_N15
\C~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_C,
	o => \C~input_o\);

-- Location: IOIBUF_X53_Y17_N15
\A~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_A,
	o => \A~input_o\);

-- Location: LCCOMB_X26_Y32_N8
\b2~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \b2~0_combout\ = (\A~input_o\ & ((\B~input_o\) # (\C~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111101000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \b2~0_combout\);

-- Location: IOIBUF_X0_Y16_N22
\D~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_D,
	o => \D~input_o\);

-- Location: LCCOMB_X26_Y32_N10
\a1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \a1~0_combout\ = (\B~input_o\ & ((\D~input_o\) # (\C~input_o\ $ (\A~input_o\)))) # (!\B~input_o\ & ((\C~input_o\ $ (\A~input_o\)) # (!\D~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1001111111111001",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \a1~0_combout\);

-- Location: LCCOMB_X26_Y32_N12
\b1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \b1~0_combout\ = (\C~input_o\ $ (((\A~input_o\) # (!\D~input_o\)))) # (!\B~input_o\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101111111010111",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \b1~0_combout\);

-- Location: LCCOMB_X26_Y32_N22
\c1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \c1~0_combout\ = (\D~input_o\) # ((\B~input_o\ & ((\C~input_o\) # (!\A~input_o\))) # (!\B~input_o\ & ((\A~input_o\) # (!\C~input_o\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1111110111101111",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \c1~0_combout\);

-- Location: LCCOMB_X26_Y32_N0
\d1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \d1~0_combout\ = (\B~input_o\ & ((\D~input_o\ & ((\A~input_o\) # (!\C~input_o\))) # (!\D~input_o\ & (\C~input_o\ $ (\A~input_o\))))) # (!\B~input_o\ & ((\C~input_o\ $ (\A~input_o\)) # (!\D~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1001111101111001",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \d1~0_combout\);

-- Location: LCCOMB_X26_Y32_N2
\e1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \e1~0_combout\ = (!\D~input_o\ & ((\C~input_o\ $ (\A~input_o\)) # (!\B~input_o\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0001001100110001",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \e1~0_combout\);

-- Location: LCCOMB_X26_Y32_N4
\f1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \f1~0_combout\ = (\B~input_o\ & (((\C~input_o\) # (!\A~input_o\)))) # (!\B~input_o\ & ((\D~input_o\ & (!\C~input_o\ & \A~input_o\)) # (!\D~input_o\ & ((\A~input_o\) # (!\C~input_o\)))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1011010110101011",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \f1~0_combout\);

-- Location: LCCOMB_X26_Y32_N6
\g1~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \g1~0_combout\ = (\B~input_o\ & (((\A~input_o\) # (!\C~input_o\)) # (!\D~input_o\))) # (!\B~input_o\ & ((\C~input_o\ $ (\A~input_o\))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010111101111010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \B~input_o\,
	datab => \D~input_o\,
	datac => \C~input_o\,
	datad => \A~input_o\,
	combout => \g1~0_combout\);

ww_b2 <= \b2~output_o\;

ww_c2 <= \c2~output_o\;

ww_a1 <= \a1~output_o\;

ww_b1 <= \b1~output_o\;

ww_c1 <= \c1~output_o\;

ww_d1 <= \d1~output_o\;

ww_e1 <= \e1~output_o\;

ww_f1 <= \f1~output_o\;

ww_g1 <= \g1~output_o\;

ww_LED_A <= \LED_A~output_o\;

ww_LED_B <= \LED_B~output_o\;

ww_LED_C <= \LED_C~output_o\;

ww_LED_D <= \LED_D~output_o\;
END structure;


