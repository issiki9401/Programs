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

-- DATE "05/11/2026 18:14:50"

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


LIBRARY ALTERA;
LIBRARY CYCLONEIVE;
LIBRARY IEEE;
USE ALTERA.ALTERA_PRIMITIVES_COMPONENTS.ALL;
USE CYCLONEIVE.CYCLONEIVE_COMPONENTS.ALL;
USE IEEE.STD_LOGIC_1164.ALL;

ENTITY 	LED_Flash IS
    PORT (
	clk : IN std_logic;
	rst : IN std_logic;
	X : IN std_logic;
	Q : BUFFER std_logic_vector(1 DOWNTO 0)
	);
END LED_Flash;

-- Design Ports Information
-- Q[0]	=>  Location: PIN_A15,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- Q[1]	=>  Location: PIN_A13,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- rst	=>  Location: PIN_M15,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- X	=>  Location: PIN_M1,	 I/O Standard: 2.5 V,	 Current Strength: Default
-- clk	=>  Location: PIN_R8,	 I/O Standard: 2.5 V,	 Current Strength: Default


ARCHITECTURE structure OF LED_Flash IS
SIGNAL gnd : std_logic := '0';
SIGNAL vcc : std_logic := '1';
SIGNAL unknown : std_logic := 'X';
SIGNAL devoe : std_logic := '1';
SIGNAL devclrn : std_logic := '1';
SIGNAL devpor : std_logic := '1';
SIGNAL ww_devoe : std_logic;
SIGNAL ww_devclrn : std_logic;
SIGNAL ww_devpor : std_logic;
SIGNAL ww_clk : std_logic;
SIGNAL ww_rst : std_logic;
SIGNAL ww_X : std_logic;
SIGNAL ww_Q : std_logic_vector(1 DOWNTO 0);
SIGNAL \U0|clk_temp~clkctrl_INCLK_bus\ : std_logic_vector(3 DOWNTO 0);
SIGNAL \clk~inputclkctrl_INCLK_bus\ : std_logic_vector(3 DOWNTO 0);
SIGNAL \rst~inputclkctrl_INCLK_bus\ : std_logic_vector(3 DOWNTO 0);
SIGNAL \Q[0]~output_o\ : std_logic;
SIGNAL \Q[1]~output_o\ : std_logic;
SIGNAL \clk~input_o\ : std_logic;
SIGNAL \clk~inputclkctrl_outclk\ : std_logic;
SIGNAL \U0|Add0~0_combout\ : std_logic;
SIGNAL \U0|cnt~9_combout\ : std_logic;
SIGNAL \U0|Add0~1\ : std_logic;
SIGNAL \U0|Add0~2_combout\ : std_logic;
SIGNAL \U0|Add0~3\ : std_logic;
SIGNAL \U0|Add0~4_combout\ : std_logic;
SIGNAL \U0|Add0~5\ : std_logic;
SIGNAL \U0|Add0~6_combout\ : std_logic;
SIGNAL \U0|Add0~7\ : std_logic;
SIGNAL \U0|Add0~8_combout\ : std_logic;
SIGNAL \U0|Add0~9\ : std_logic;
SIGNAL \U0|Add0~10_combout\ : std_logic;
SIGNAL \U0|Add0~11\ : std_logic;
SIGNAL \U0|Add0~12_combout\ : std_logic;
SIGNAL \U0|cnt~8_combout\ : std_logic;
SIGNAL \U0|Add0~13\ : std_logic;
SIGNAL \U0|Add0~14_combout\ : std_logic;
SIGNAL \U0|Add0~15\ : std_logic;
SIGNAL \U0|Add0~16_combout\ : std_logic;
SIGNAL \U0|Add0~17\ : std_logic;
SIGNAL \U0|Add0~18_combout\ : std_logic;
SIGNAL \U0|Add0~19\ : std_logic;
SIGNAL \U0|Add0~20_combout\ : std_logic;
SIGNAL \U0|Add0~21\ : std_logic;
SIGNAL \U0|Add0~22_combout\ : std_logic;
SIGNAL \U0|cnt~10_combout\ : std_logic;
SIGNAL \U0|Equal0~2_combout\ : std_logic;
SIGNAL \U0|Add0~23\ : std_logic;
SIGNAL \U0|Add0~24_combout\ : std_logic;
SIGNAL \U0|cnt~11_combout\ : std_logic;
SIGNAL \U0|Add0~25\ : std_logic;
SIGNAL \U0|Add0~26_combout\ : std_logic;
SIGNAL \U0|cnt~12_combout\ : std_logic;
SIGNAL \U0|Add0~27\ : std_logic;
SIGNAL \U0|Add0~28_combout\ : std_logic;
SIGNAL \U0|cnt~13_combout\ : std_logic;
SIGNAL \U0|Add0~29\ : std_logic;
SIGNAL \U0|Add0~30_combout\ : std_logic;
SIGNAL \U0|Equal0~3_combout\ : std_logic;
SIGNAL \U0|Equal0~1_combout\ : std_logic;
SIGNAL \U0|Equal0~0_combout\ : std_logic;
SIGNAL \U0|Equal0~4_combout\ : std_logic;
SIGNAL \U0|Add0~31\ : std_logic;
SIGNAL \U0|Add0~32_combout\ : std_logic;
SIGNAL \U0|cnt~14_combout\ : std_logic;
SIGNAL \U0|Add0~33\ : std_logic;
SIGNAL \U0|Add0~34_combout\ : std_logic;
SIGNAL \U0|Add0~35\ : std_logic;
SIGNAL \U0|Add0~36_combout\ : std_logic;
SIGNAL \U0|cnt~15_combout\ : std_logic;
SIGNAL \U0|Add0~37\ : std_logic;
SIGNAL \U0|Add0~38_combout\ : std_logic;
SIGNAL \U0|cnt~16_combout\ : std_logic;
SIGNAL \U0|Add0~39\ : std_logic;
SIGNAL \U0|Add0~40_combout\ : std_logic;
SIGNAL \U0|cnt~17_combout\ : std_logic;
SIGNAL \U0|Add0~41\ : std_logic;
SIGNAL \U0|Add0~42_combout\ : std_logic;
SIGNAL \U0|cnt~18_combout\ : std_logic;
SIGNAL \U0|Add0~43\ : std_logic;
SIGNAL \U0|Add0~44_combout\ : std_logic;
SIGNAL \U0|cnt~19_combout\ : std_logic;
SIGNAL \U0|Add0~45\ : std_logic;
SIGNAL \U0|Add0~46_combout\ : std_logic;
SIGNAL \U0|Add0~47\ : std_logic;
SIGNAL \U0|Add0~48_combout\ : std_logic;
SIGNAL \U0|cnt~20_combout\ : std_logic;
SIGNAL \U0|Add0~49\ : std_logic;
SIGNAL \U0|Add0~50_combout\ : std_logic;
SIGNAL \U0|Equal0~6_combout\ : std_logic;
SIGNAL \U0|Equal0~5_combout\ : std_logic;
SIGNAL \U0|Equal0~7_combout\ : std_logic;
SIGNAL \U0|clk_temp~0_combout\ : std_logic;
SIGNAL \U0|clk_temp~feeder_combout\ : std_logic;
SIGNAL \U0|clk_temp~q\ : std_logic;
SIGNAL \U0|clk_temp~clkctrl_outclk\ : std_logic;
SIGNAL \U2|Q~0_combout\ : std_logic;
SIGNAL \rst~input_o\ : std_logic;
SIGNAL \rst~inputclkctrl_outclk\ : std_logic;
SIGNAL \U2|Q~q\ : std_logic;
SIGNAL \X~input_o\ : std_logic;
SIGNAL \Anext~0_combout\ : std_logic;
SIGNAL \U1|Q~q\ : std_logic;
SIGNAL \U0|cnt\ : std_logic_vector(25 DOWNTO 0);
SIGNAL \ALT_INV_rst~inputclkctrl_outclk\ : std_logic;

COMPONENT hard_block
    PORT (
	devoe : IN std_logic;
	devclrn : IN std_logic;
	devpor : IN std_logic);
END COMPONENT;

BEGIN

ww_clk <= clk;
ww_rst <= rst;
ww_X <= X;
Q <= ww_Q;
ww_devoe <= devoe;
ww_devclrn <= devclrn;
ww_devpor <= devpor;

\U0|clk_temp~clkctrl_INCLK_bus\ <= (vcc & vcc & vcc & \U0|clk_temp~q\);

\clk~inputclkctrl_INCLK_bus\ <= (vcc & vcc & vcc & \clk~input_o\);

\rst~inputclkctrl_INCLK_bus\ <= (vcc & vcc & vcc & \rst~input_o\);
\ALT_INV_rst~inputclkctrl_outclk\ <= NOT \rst~inputclkctrl_outclk\;
auto_generated_inst : hard_block
PORT MAP (
	devoe => ww_devoe,
	devclrn => ww_devclrn,
	devpor => ww_devpor);

-- Location: IOOBUF_X38_Y34_N16
\Q[0]~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U2|Q~q\,
	devoe => ww_devoe,
	o => \Q[0]~output_o\);

-- Location: IOOBUF_X49_Y34_N2
\Q[1]~output\ : cycloneive_io_obuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	open_drain_output => "false")
-- pragma translate_on
PORT MAP (
	i => \U1|Q~q\,
	devoe => ww_devoe,
	o => \Q[1]~output_o\);

-- Location: IOIBUF_X27_Y0_N22
\clk~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_clk,
	o => \clk~input_o\);

-- Location: CLKCTRL_G18
\clk~inputclkctrl\ : cycloneive_clkctrl
-- pragma translate_off
GENERIC MAP (
	clock_type => "global clock",
	ena_register_mode => "none")
-- pragma translate_on
PORT MAP (
	inclk => \clk~inputclkctrl_INCLK_bus\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	outclk => \clk~inputclkctrl_outclk\);

-- Location: LCCOMB_X2_Y13_N6
\U0|Add0~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~0_combout\ = \U0|cnt\(0) $ (VCC)
-- \U0|Add0~1\ = CARRY(\U0|cnt\(0))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011001111001100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(0),
	datad => VCC,
	combout => \U0|Add0~0_combout\,
	cout => \U0|Add0~1\);

-- Location: LCCOMB_X2_Y13_N2
\U0|cnt~9\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~9_combout\ = (\U0|Add0~0_combout\ & ((!\U0|Equal0~7_combout\) # (!\U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000101010101010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Add0~0_combout\,
	datac => \U0|Equal0~4_combout\,
	datad => \U0|Equal0~7_combout\,
	combout => \U0|cnt~9_combout\);

-- Location: FF_X2_Y13_N3
\U0|cnt[0]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~9_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(0));

-- Location: LCCOMB_X2_Y13_N8
\U0|Add0~2\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~2_combout\ = (\U0|cnt\(1) & (!\U0|Add0~1\)) # (!\U0|cnt\(1) & ((\U0|Add0~1\) # (GND)))
-- \U0|Add0~3\ = CARRY((!\U0|Add0~1\) # (!\U0|cnt\(1)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(1),
	datad => VCC,
	cin => \U0|Add0~1\,
	combout => \U0|Add0~2_combout\,
	cout => \U0|Add0~3\);

-- Location: FF_X2_Y13_N9
\U0|cnt[1]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~2_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(1));

-- Location: LCCOMB_X2_Y13_N10
\U0|Add0~4\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~4_combout\ = (\U0|cnt\(2) & (\U0|Add0~3\ $ (GND))) # (!\U0|cnt\(2) & (!\U0|Add0~3\ & VCC))
-- \U0|Add0~5\ = CARRY((\U0|cnt\(2) & !\U0|Add0~3\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(2),
	datad => VCC,
	cin => \U0|Add0~3\,
	combout => \U0|Add0~4_combout\,
	cout => \U0|Add0~5\);

-- Location: FF_X2_Y13_N11
\U0|cnt[2]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~4_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(2));

-- Location: LCCOMB_X2_Y13_N12
\U0|Add0~6\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~6_combout\ = (\U0|cnt\(3) & (!\U0|Add0~5\)) # (!\U0|cnt\(3) & ((\U0|Add0~5\) # (GND)))
-- \U0|Add0~7\ = CARRY((!\U0|Add0~5\) # (!\U0|cnt\(3)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101101001011111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(3),
	datad => VCC,
	cin => \U0|Add0~5\,
	combout => \U0|Add0~6_combout\,
	cout => \U0|Add0~7\);

-- Location: FF_X2_Y13_N13
\U0|cnt[3]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~6_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(3));

-- Location: LCCOMB_X2_Y13_N14
\U0|Add0~8\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~8_combout\ = (\U0|cnt\(4) & (\U0|Add0~7\ $ (GND))) # (!\U0|cnt\(4) & (!\U0|Add0~7\ & VCC))
-- \U0|Add0~9\ = CARRY((\U0|cnt\(4) & !\U0|Add0~7\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100001100001100",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(4),
	datad => VCC,
	cin => \U0|Add0~7\,
	combout => \U0|Add0~8_combout\,
	cout => \U0|Add0~9\);

-- Location: FF_X2_Y13_N15
\U0|cnt[4]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~8_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(4));

-- Location: LCCOMB_X2_Y13_N16
\U0|Add0~10\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~10_combout\ = (\U0|cnt\(5) & (!\U0|Add0~9\)) # (!\U0|cnt\(5) & ((\U0|Add0~9\) # (GND)))
-- \U0|Add0~11\ = CARRY((!\U0|Add0~9\) # (!\U0|cnt\(5)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(5),
	datad => VCC,
	cin => \U0|Add0~9\,
	combout => \U0|Add0~10_combout\,
	cout => \U0|Add0~11\);

-- Location: FF_X2_Y13_N17
\U0|cnt[5]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~10_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(5));

-- Location: LCCOMB_X2_Y13_N18
\U0|Add0~12\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~12_combout\ = (\U0|cnt\(6) & (\U0|Add0~11\ $ (GND))) # (!\U0|cnt\(6) & (!\U0|Add0~11\ & VCC))
-- \U0|Add0~13\ = CARRY((\U0|cnt\(6) & !\U0|Add0~11\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100001100001100",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(6),
	datad => VCC,
	cin => \U0|Add0~11\,
	combout => \U0|Add0~12_combout\,
	cout => \U0|Add0~13\);

-- Location: LCCOMB_X2_Y13_N4
\U0|cnt~8\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~8_combout\ = (\U0|Add0~12_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~7_combout\,
	datac => \U0|Equal0~4_combout\,
	datad => \U0|Add0~12_combout\,
	combout => \U0|cnt~8_combout\);

-- Location: FF_X2_Y13_N5
\U0|cnt[6]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~8_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(6));

-- Location: LCCOMB_X2_Y13_N20
\U0|Add0~14\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~14_combout\ = (\U0|cnt\(7) & (!\U0|Add0~13\)) # (!\U0|cnt\(7) & ((\U0|Add0~13\) # (GND)))
-- \U0|Add0~15\ = CARRY((!\U0|Add0~13\) # (!\U0|cnt\(7)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(7),
	datad => VCC,
	cin => \U0|Add0~13\,
	combout => \U0|Add0~14_combout\,
	cout => \U0|Add0~15\);

-- Location: FF_X2_Y13_N21
\U0|cnt[7]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~14_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(7));

-- Location: LCCOMB_X2_Y13_N22
\U0|Add0~16\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~16_combout\ = (\U0|cnt\(8) & (\U0|Add0~15\ $ (GND))) # (!\U0|cnt\(8) & (!\U0|Add0~15\ & VCC))
-- \U0|Add0~17\ = CARRY((\U0|cnt\(8) & !\U0|Add0~15\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(8),
	datad => VCC,
	cin => \U0|Add0~15\,
	combout => \U0|Add0~16_combout\,
	cout => \U0|Add0~17\);

-- Location: FF_X2_Y13_N23
\U0|cnt[8]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~16_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(8));

-- Location: LCCOMB_X2_Y13_N24
\U0|Add0~18\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~18_combout\ = (\U0|cnt\(9) & (!\U0|Add0~17\)) # (!\U0|cnt\(9) & ((\U0|Add0~17\) # (GND)))
-- \U0|Add0~19\ = CARRY((!\U0|Add0~17\) # (!\U0|cnt\(9)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(9),
	datad => VCC,
	cin => \U0|Add0~17\,
	combout => \U0|Add0~18_combout\,
	cout => \U0|Add0~19\);

-- Location: FF_X2_Y13_N25
\U0|cnt[9]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~18_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(9));

-- Location: LCCOMB_X2_Y13_N26
\U0|Add0~20\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~20_combout\ = (\U0|cnt\(10) & (\U0|Add0~19\ $ (GND))) # (!\U0|cnt\(10) & (!\U0|Add0~19\ & VCC))
-- \U0|Add0~21\ = CARRY((\U0|cnt\(10) & !\U0|Add0~19\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(10),
	datad => VCC,
	cin => \U0|Add0~19\,
	combout => \U0|Add0~20_combout\,
	cout => \U0|Add0~21\);

-- Location: FF_X2_Y13_N27
\U0|cnt[10]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~20_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(10));

-- Location: LCCOMB_X2_Y13_N28
\U0|Add0~22\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~22_combout\ = (\U0|cnt\(11) & (!\U0|Add0~21\)) # (!\U0|cnt\(11) & ((\U0|Add0~21\) # (GND)))
-- \U0|Add0~23\ = CARRY((!\U0|Add0~21\) # (!\U0|cnt\(11)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(11),
	datad => VCC,
	cin => \U0|Add0~21\,
	combout => \U0|Add0~22_combout\,
	cout => \U0|Add0~23\);

-- Location: LCCOMB_X1_Y12_N24
\U0|cnt~10\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~10_combout\ = (\U0|Add0~22_combout\ & ((!\U0|Equal0~7_combout\) # (!\U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~4_combout\,
	datac => \U0|Equal0~7_combout\,
	datad => \U0|Add0~22_combout\,
	combout => \U0|cnt~10_combout\);

-- Location: FF_X1_Y12_N25
\U0|cnt[11]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~10_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(11));

-- Location: LCCOMB_X1_Y12_N12
\U0|Equal0~2\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~2_combout\ = (!\U0|cnt\(9) & (\U0|cnt\(11) & (!\U0|cnt\(8) & !\U0|cnt\(10))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000000000100",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(9),
	datab => \U0|cnt\(11),
	datac => \U0|cnt\(8),
	datad => \U0|cnt\(10),
	combout => \U0|Equal0~2_combout\);

-- Location: LCCOMB_X2_Y13_N30
\U0|Add0~24\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~24_combout\ = (\U0|cnt\(12) & (\U0|Add0~23\ $ (GND))) # (!\U0|cnt\(12) & (!\U0|Add0~23\ & VCC))
-- \U0|Add0~25\ = CARRY((\U0|cnt\(12) & !\U0|Add0~23\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(12),
	datad => VCC,
	cin => \U0|Add0~23\,
	combout => \U0|Add0~24_combout\,
	cout => \U0|Add0~25\);

-- Location: LCCOMB_X1_Y12_N2
\U0|cnt~11\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~11_combout\ = (\U0|Add0~24_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101000011110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Equal0~7_combout\,
	datac => \U0|Add0~24_combout\,
	datad => \U0|Equal0~4_combout\,
	combout => \U0|cnt~11_combout\);

-- Location: FF_X1_Y12_N3
\U0|cnt[12]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~11_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(12));

-- Location: LCCOMB_X2_Y12_N0
\U0|Add0~26\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~26_combout\ = (\U0|cnt\(13) & (!\U0|Add0~25\)) # (!\U0|cnt\(13) & ((\U0|Add0~25\) # (GND)))
-- \U0|Add0~27\ = CARRY((!\U0|Add0~25\) # (!\U0|cnt\(13)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(13),
	datad => VCC,
	cin => \U0|Add0~25\,
	combout => \U0|Add0~26_combout\,
	cout => \U0|Add0~27\);

-- Location: LCCOMB_X1_Y12_N4
\U0|cnt~12\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~12_combout\ = (\U0|Add0~26_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101000011110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Equal0~7_combout\,
	datac => \U0|Add0~26_combout\,
	datad => \U0|Equal0~4_combout\,
	combout => \U0|cnt~12_combout\);

-- Location: FF_X1_Y12_N5
\U0|cnt[13]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~12_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(13));

-- Location: LCCOMB_X2_Y12_N2
\U0|Add0~28\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~28_combout\ = (\U0|cnt\(14) & (\U0|Add0~27\ $ (GND))) # (!\U0|cnt\(14) & (!\U0|Add0~27\ & VCC))
-- \U0|Add0~29\ = CARRY((\U0|cnt\(14) & !\U0|Add0~27\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(14),
	datad => VCC,
	cin => \U0|Add0~27\,
	combout => \U0|Add0~28_combout\,
	cout => \U0|Add0~29\);

-- Location: LCCOMB_X1_Y12_N26
\U0|cnt~13\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~13_combout\ = (\U0|Add0~28_combout\ & ((!\U0|Equal0~7_combout\) # (!\U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~4_combout\,
	datac => \U0|Equal0~7_combout\,
	datad => \U0|Add0~28_combout\,
	combout => \U0|cnt~13_combout\);

-- Location: FF_X1_Y12_N27
\U0|cnt[14]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~13_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(14));

-- Location: LCCOMB_X2_Y12_N4
\U0|Add0~30\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~30_combout\ = (\U0|cnt\(15) & (!\U0|Add0~29\)) # (!\U0|cnt\(15) & ((\U0|Add0~29\) # (GND)))
-- \U0|Add0~31\ = CARRY((!\U0|Add0~29\) # (!\U0|cnt\(15)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(15),
	datad => VCC,
	cin => \U0|Add0~29\,
	combout => \U0|Add0~30_combout\,
	cout => \U0|Add0~31\);

-- Location: FF_X2_Y12_N5
\U0|cnt[15]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~30_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(15));

-- Location: LCCOMB_X1_Y12_N20
\U0|Equal0~3\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~3_combout\ = (\U0|cnt\(14) & (\U0|cnt\(12) & (\U0|cnt\(13) & !\U0|cnt\(15))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000010000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(14),
	datab => \U0|cnt\(12),
	datac => \U0|cnt\(13),
	datad => \U0|cnt\(15),
	combout => \U0|Equal0~3_combout\);

-- Location: LCCOMB_X2_Y13_N0
\U0|Equal0~1\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~1_combout\ = (!\U0|cnt\(3) & (!\U0|cnt\(0) & (!\U0|cnt\(2) & !\U0|cnt\(1))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000000000001",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(3),
	datab => \U0|cnt\(0),
	datac => \U0|cnt\(2),
	datad => \U0|cnt\(1),
	combout => \U0|Equal0~1_combout\);

-- Location: LCCOMB_X1_Y13_N24
\U0|Equal0~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~0_combout\ = (!\U0|cnt\(7) & (!\U0|cnt\(5) & (!\U0|cnt\(4) & \U0|cnt\(6))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000000100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(7),
	datab => \U0|cnt\(5),
	datac => \U0|cnt\(4),
	datad => \U0|cnt\(6),
	combout => \U0|Equal0~0_combout\);

-- Location: LCCOMB_X1_Y12_N0
\U0|Equal0~4\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~4_combout\ = (\U0|Equal0~2_combout\ & (\U0|Equal0~3_combout\ & (\U0|Equal0~1_combout\ & \U0|Equal0~0_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1000000000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Equal0~2_combout\,
	datab => \U0|Equal0~3_combout\,
	datac => \U0|Equal0~1_combout\,
	datad => \U0|Equal0~0_combout\,
	combout => \U0|Equal0~4_combout\);

-- Location: LCCOMB_X2_Y12_N6
\U0|Add0~32\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~32_combout\ = (\U0|cnt\(16) & (\U0|Add0~31\ $ (GND))) # (!\U0|cnt\(16) & (!\U0|Add0~31\ & VCC))
-- \U0|Add0~33\ = CARRY((\U0|cnt\(16) & !\U0|Add0~31\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(16),
	datad => VCC,
	cin => \U0|Add0~31\,
	combout => \U0|Add0~32_combout\,
	cout => \U0|Add0~33\);

-- Location: LCCOMB_X1_Y12_N10
\U0|cnt~14\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~14_combout\ = (\U0|Add0~32_combout\ & ((!\U0|Equal0~7_combout\) # (!\U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~4_combout\,
	datac => \U0|Equal0~7_combout\,
	datad => \U0|Add0~32_combout\,
	combout => \U0|cnt~14_combout\);

-- Location: FF_X1_Y12_N11
\U0|cnt[16]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~14_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(16));

-- Location: LCCOMB_X2_Y12_N8
\U0|Add0~34\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~34_combout\ = (\U0|cnt\(17) & (!\U0|Add0~33\)) # (!\U0|cnt\(17) & ((\U0|Add0~33\) # (GND)))
-- \U0|Add0~35\ = CARRY((!\U0|Add0~33\) # (!\U0|cnt\(17)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(17),
	datad => VCC,
	cin => \U0|Add0~33\,
	combout => \U0|Add0~34_combout\,
	cout => \U0|Add0~35\);

-- Location: FF_X2_Y12_N9
\U0|cnt[17]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~34_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(17));

-- Location: LCCOMB_X2_Y12_N10
\U0|Add0~36\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~36_combout\ = (\U0|cnt\(18) & (\U0|Add0~35\ $ (GND))) # (!\U0|cnt\(18) & (!\U0|Add0~35\ & VCC))
-- \U0|Add0~37\ = CARRY((\U0|cnt\(18) & !\U0|Add0~35\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1100001100001100",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(18),
	datad => VCC,
	cin => \U0|Add0~35\,
	combout => \U0|Add0~36_combout\,
	cout => \U0|Add0~37\);

-- Location: LCCOMB_X1_Y12_N6
\U0|cnt~15\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~15_combout\ = (\U0|Add0~36_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101000011110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Equal0~7_combout\,
	datac => \U0|Add0~36_combout\,
	datad => \U0|Equal0~4_combout\,
	combout => \U0|cnt~15_combout\);

-- Location: FF_X1_Y12_N7
\U0|cnt[18]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~15_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(18));

-- Location: LCCOMB_X2_Y12_N12
\U0|Add0~38\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~38_combout\ = (\U0|cnt\(19) & (!\U0|Add0~37\)) # (!\U0|cnt\(19) & ((\U0|Add0~37\) # (GND)))
-- \U0|Add0~39\ = CARRY((!\U0|Add0~37\) # (!\U0|cnt\(19)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(19),
	datad => VCC,
	cin => \U0|Add0~37\,
	combout => \U0|Add0~38_combout\,
	cout => \U0|Add0~39\);

-- Location: LCCOMB_X1_Y12_N22
\U0|cnt~16\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~16_combout\ = (\U0|Add0~38_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101000011110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Equal0~7_combout\,
	datac => \U0|Add0~38_combout\,
	datad => \U0|Equal0~4_combout\,
	combout => \U0|cnt~16_combout\);

-- Location: FF_X1_Y12_N23
\U0|cnt[19]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~16_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(19));

-- Location: LCCOMB_X2_Y12_N14
\U0|Add0~40\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~40_combout\ = (\U0|cnt\(20) & (\U0|Add0~39\ $ (GND))) # (!\U0|cnt\(20) & (!\U0|Add0~39\ & VCC))
-- \U0|Add0~41\ = CARRY((\U0|cnt\(20) & !\U0|Add0~39\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(20),
	datad => VCC,
	cin => \U0|Add0~39\,
	combout => \U0|Add0~40_combout\,
	cout => \U0|Add0~41\);

-- Location: LCCOMB_X2_Y12_N26
\U0|cnt~17\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~17_combout\ = (\U0|Add0~40_combout\ & ((!\U0|Equal0~7_combout\) # (!\U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101000011110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|Equal0~4_combout\,
	datac => \U0|Add0~40_combout\,
	datad => \U0|Equal0~7_combout\,
	combout => \U0|cnt~17_combout\);

-- Location: FF_X2_Y12_N27
\U0|cnt[20]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~17_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(20));

-- Location: LCCOMB_X2_Y12_N16
\U0|Add0~42\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~42_combout\ = (\U0|cnt\(21) & (!\U0|Add0~41\)) # (!\U0|cnt\(21) & ((\U0|Add0~41\) # (GND)))
-- \U0|Add0~43\ = CARRY((!\U0|Add0~41\) # (!\U0|cnt\(21)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(21),
	datad => VCC,
	cin => \U0|Add0~41\,
	combout => \U0|Add0~42_combout\,
	cout => \U0|Add0~43\);

-- Location: LCCOMB_X2_Y12_N28
\U0|cnt~18\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~18_combout\ = (\U0|Add0~42_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~7_combout\,
	datac => \U0|Equal0~4_combout\,
	datad => \U0|Add0~42_combout\,
	combout => \U0|cnt~18_combout\);

-- Location: FF_X2_Y12_N29
\U0|cnt[21]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~18_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(21));

-- Location: LCCOMB_X2_Y12_N18
\U0|Add0~44\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~44_combout\ = (\U0|cnt\(22) & (\U0|Add0~43\ $ (GND))) # (!\U0|cnt\(22) & (!\U0|Add0~43\ & VCC))
-- \U0|Add0~45\ = CARRY((\U0|cnt\(22) & !\U0|Add0~43\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(22),
	datad => VCC,
	cin => \U0|Add0~43\,
	combout => \U0|Add0~44_combout\,
	cout => \U0|Add0~45\);

-- Location: LCCOMB_X2_Y12_N30
\U0|cnt~19\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~19_combout\ = (\U0|Add0~44_combout\ & ((!\U0|Equal0~4_combout\) # (!\U0|Equal0~7_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~7_combout\,
	datac => \U0|Equal0~4_combout\,
	datad => \U0|Add0~44_combout\,
	combout => \U0|cnt~19_combout\);

-- Location: FF_X2_Y12_N31
\U0|cnt[22]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~19_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(22));

-- Location: LCCOMB_X2_Y12_N20
\U0|Add0~46\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~46_combout\ = (\U0|cnt\(23) & (!\U0|Add0~45\)) # (!\U0|cnt\(23) & ((\U0|Add0~45\) # (GND)))
-- \U0|Add0~47\ = CARRY((!\U0|Add0~45\) # (!\U0|cnt\(23)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011110000111111",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datab => \U0|cnt\(23),
	datad => VCC,
	cin => \U0|Add0~45\,
	combout => \U0|Add0~46_combout\,
	cout => \U0|Add0~47\);

-- Location: FF_X2_Y12_N21
\U0|cnt[23]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~46_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(23));

-- Location: LCCOMB_X2_Y12_N22
\U0|Add0~48\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~48_combout\ = (\U0|cnt\(24) & (\U0|Add0~47\ $ (GND))) # (!\U0|cnt\(24) & (!\U0|Add0~47\ & VCC))
-- \U0|Add0~49\ = CARRY((\U0|cnt\(24) & !\U0|Add0~47\))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010010100001010",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(24),
	datad => VCC,
	cin => \U0|Add0~47\,
	combout => \U0|Add0~48_combout\,
	cout => \U0|Add0~49\);

-- Location: LCCOMB_X1_Y12_N14
\U0|cnt~20\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|cnt~20_combout\ = (\U0|Add0~48_combout\ & ((!\U0|Equal0~7_combout\) # (!\U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0011111100000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datab => \U0|Equal0~4_combout\,
	datac => \U0|Equal0~7_combout\,
	datad => \U0|Add0~48_combout\,
	combout => \U0|cnt~20_combout\);

-- Location: FF_X1_Y12_N15
\U0|cnt[24]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|cnt~20_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(24));

-- Location: LCCOMB_X2_Y12_N24
\U0|Add0~50\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Add0~50_combout\ = \U0|Add0~49\ $ (\U0|cnt\(25))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000111111110000",
	sum_lutc_input => "cin")
-- pragma translate_on
PORT MAP (
	datad => \U0|cnt\(25),
	cin => \U0|Add0~49\,
	combout => \U0|Add0~50_combout\);

-- Location: FF_X2_Y12_N25
\U0|cnt[25]\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|Add0~50_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|cnt\(25));

-- Location: LCCOMB_X1_Y12_N18
\U0|Equal0~6\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~6_combout\ = (!\U0|cnt\(23) & (\U0|cnt\(21) & (\U0|cnt\(22) & \U0|cnt\(20))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0100000000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(23),
	datab => \U0|cnt\(21),
	datac => \U0|cnt\(22),
	datad => \U0|cnt\(20),
	combout => \U0|Equal0~6_combout\);

-- Location: LCCOMB_X1_Y12_N28
\U0|Equal0~5\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~5_combout\ = (\U0|cnt\(16) & (!\U0|cnt\(17) & (\U0|cnt\(19) & \U0|cnt\(18))))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0010000000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(16),
	datab => \U0|cnt\(17),
	datac => \U0|cnt\(19),
	datad => \U0|cnt\(18),
	combout => \U0|Equal0~5_combout\);

-- Location: LCCOMB_X1_Y12_N30
\U0|Equal0~7\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|Equal0~7_combout\ = (!\U0|cnt\(25) & (\U0|Equal0~6_combout\ & (\U0|cnt\(24) & \U0|Equal0~5_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0100000000000000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|cnt\(25),
	datab => \U0|Equal0~6_combout\,
	datac => \U0|cnt\(24),
	datad => \U0|Equal0~5_combout\,
	combout => \U0|Equal0~7_combout\);

-- Location: LCCOMB_X1_Y12_N8
\U0|clk_temp~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|clk_temp~0_combout\ = \U0|clk_temp~q\ $ (((\U0|Equal0~7_combout\ & \U0|Equal0~4_combout\)))

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0101101010101010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|clk_temp~q\,
	datac => \U0|Equal0~7_combout\,
	datad => \U0|Equal0~4_combout\,
	combout => \U0|clk_temp~0_combout\);

-- Location: LCCOMB_X1_Y12_N16
\U0|clk_temp~feeder\ : cycloneive_lcell_comb
-- Equation(s):
-- \U0|clk_temp~feeder_combout\ = \U0|clk_temp~0_combout\

-- pragma translate_off
GENERIC MAP (
	lut_mask => "1010101010101010",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	dataa => \U0|clk_temp~0_combout\,
	combout => \U0|clk_temp~feeder_combout\);

-- Location: FF_X1_Y12_N17
\U0|clk_temp\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \clk~inputclkctrl_outclk\,
	d => \U0|clk_temp~feeder_combout\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U0|clk_temp~q\);

-- Location: CLKCTRL_G4
\U0|clk_temp~clkctrl\ : cycloneive_clkctrl
-- pragma translate_off
GENERIC MAP (
	clock_type => "global clock",
	ena_register_mode => "none")
-- pragma translate_on
PORT MAP (
	inclk => \U0|clk_temp~clkctrl_INCLK_bus\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	outclk => \U0|clk_temp~clkctrl_outclk\);

-- Location: LCCOMB_X43_Y30_N0
\U2|Q~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \U2|Q~0_combout\ = !\U2|Q~q\

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000111100001111",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datac => \U2|Q~q\,
	combout => \U2|Q~0_combout\);

-- Location: IOIBUF_X53_Y17_N15
\rst~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_rst,
	o => \rst~input_o\);

-- Location: CLKCTRL_G9
\rst~inputclkctrl\ : cycloneive_clkctrl
-- pragma translate_off
GENERIC MAP (
	clock_type => "global clock",
	ena_register_mode => "none")
-- pragma translate_on
PORT MAP (
	inclk => \rst~inputclkctrl_INCLK_bus\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	outclk => \rst~inputclkctrl_outclk\);

-- Location: FF_X43_Y30_N1
\U2|Q\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \U0|clk_temp~clkctrl_outclk\,
	d => \U2|Q~0_combout\,
	clrn => \ALT_INV_rst~inputclkctrl_outclk\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U2|Q~q\);

-- Location: IOIBUF_X0_Y16_N22
\X~input\ : cycloneive_io_ibuf
-- pragma translate_off
GENERIC MAP (
	bus_hold => "false",
	simulate_z_as => "z")
-- pragma translate_on
PORT MAP (
	i => ww_X,
	o => \X~input_o\);

-- Location: LCCOMB_X43_Y30_N2
\Anext~0\ : cycloneive_lcell_comb
-- Equation(s):
-- \Anext~0_combout\ = \X~input_o\ $ (\U2|Q~q\)

-- pragma translate_off
GENERIC MAP (
	lut_mask => "0000111111110000",
	sum_lutc_input => "datac")
-- pragma translate_on
PORT MAP (
	datac => \X~input_o\,
	datad => \U2|Q~q\,
	combout => \Anext~0_combout\);

-- Location: FF_X43_Y30_N3
\U1|Q\ : dffeas
-- pragma translate_off
GENERIC MAP (
	is_wysiwyg => "true",
	power_up => "low")
-- pragma translate_on
PORT MAP (
	clk => \U0|clk_temp~clkctrl_outclk\,
	d => \Anext~0_combout\,
	clrn => \ALT_INV_rst~inputclkctrl_outclk\,
	devclrn => ww_devclrn,
	devpor => ww_devpor,
	q => \U1|Q~q\);

ww_Q(0) <= \Q[0]~output_o\;

ww_Q(1) <= \Q[1]~output_o\;
END structure;


