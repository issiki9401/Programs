library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;

-- ==========================================
-- Logic Unit
-- ==========================================
entity logic_unit is
    port (
        Ain, Bin : in std_logic_vector(3 downto 0);
        F1F0     : in std_logic_vector(1 downto 0);
        Sout     : out std_logic_vector(3 downto 0)
    );
end logic_unit;

architecture arch of logic_unit is
begin
    process(Ain, Bin, F1F0)
    begin
        case F1F0 is
            when "00" => Sout <= not Ain;       -- not A
            when "01" => Sout <= not Bin;       -- not B
            when "10" => Sout <= Ain and Bin;   -- A and B
            when "11" => Sout <= Ain or Bin;    -- A or B
            when others => Sout <= "0000";
        end case;
    end process;
end arch;

-- ==========================================
-- Arithmetic Unit (In-lab 規格)
-- ==========================================
library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;

entity arithmetic_unit is
    port (
        Ain, Bin : in std_logic_vector(3 downto 0);
        F1F0     : in std_logic_vector(1 downto 0);
        Sout     : out std_logic_vector(3 downto 0);
        Overflow : out std_logic;
        Negative : out std_logic
    );
end arithmetic_unit;

architecture arch of arithmetic_unit is
    signal op1, op2 : std_logic_vector(3 downto 0);
    signal cin      : std_logic;
    signal sum      : std_logic_vector(4 downto 0);
    signal temp_S   : std_logic_vector(3 downto 0);
    signal overF    : std_logic;
begin
    -- 根據 F1F0 決定運算元 (00: -A, 01: -B, 10: A+B, 11: A-B)
    process(Ain, Bin, F1F0)
    begin
        case F1F0 is
            when "00" => op1 <= "0000"; op2 <= not Ain; cin <= '1'; -- 0 - A
            when "01" => op1 <= "0000"; op2 <= not Bin; cin <= '1'; -- 0 - B
            when "10" => op1 <= Ain;    op2 <= Bin;     cin <= '0'; -- A + B
            when "11" => op1 <= Ain;    op2 <= not Bin; cin <= '1'; -- A - B
            when others => op1 <= "0000"; op2 <= "0000"; cin <= '0';
        end case;
    end process;

    -- 加法器實作 (X + Y + Cin)
    sum <= ("0" & op1) + ("0" & op2) + cin;
    temp_S <= sum(3 downto 0);

    -- Overflow 判斷：若兩輸入同號，但與結果異號，則發生溢位
    overF <= (op1(3) and op2(3) and not temp_S(3)) or 
             (not op1(3) and not op2(3) and temp_S(3));
             
    Overflow <= overF;
    Negative <= temp_S(3) and not overF; 
    
    -- 若結果為負，則取 2 補數以顯示絕對值
    Sout <= (not temp_S) + 1 when (temp_S(3) = '1' and overF = '0') else temp_S;
end arch;

-- ==========================================
-- Display Unit
-- ==========================================
library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;

entity display is
    port (
        Sin      : in std_logic_vector(3 downto 0);
        F2       : in std_logic;
        overF    : in std_logic;
        Negative : in std_logic;
        a1, b1, c1, d1, e1, f1, g1 : out std_logic;
        g2       : out std_logic
    );
end display;

architecture arch of display is
    signal seg : std_logic_vector(6 downto 0);
begin
    -- 共陰極七段顯示器 (1為亮，0為暗)
    process(Sin, overF, F2)
    begin
        if (F2 = '1' and overF = '1') then
            seg <= "1001111"; -- 發生溢位時顯示 'E' (Error)
        else
            case Sin is
                when "0000" => seg <= "0111111"; -- 0
                when "0001" => seg <= "0000110"; -- 1
                when "0010" => seg <= "1011011"; -- 2
                when "0011" => seg <= "1001111"; -- 3
                when "0100" => seg <= "1100110"; -- 4
                when "0101" => seg <= "1101101"; -- 5
                when "0110" => seg <= "1111101"; -- 6
                when "0111" => seg <= "0100111"; -- 7
                when "1000" => seg <= "1111111"; -- 8
                when "1001" => seg <= "1101111"; -- 9
                when others => seg <= "0000000";
            end case;
        end if;
    end process;

    a1 <= seg(0); b1 <= seg(1); c1 <= seg(2); d1 <= seg(3); 
    e1 <= seg(4); f1 <= seg(5); g1 <= seg(6);
    
    -- 負號顯示 (控制第二個七段顯示器的 g 節段)
    g2 <= '1' when (F2 = '1' and Negative = '1' and overF = '0') else '0';
end arch;

-- ==========================================
-- Top-Level Entity (lab12)
-- ==========================================
library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;

entity lab12 is
    port (
        Ain, Bin : in std_logic_vector(3 downto 0);
        F        : in std_logic_vector(2 downto 0);
        Sout     : out std_logic_vector(3 downto 0);
        Overflow : out std_logic;
        Neg      : out std_logic;
        a1, b1, c1, d1, e1, f1, g1 : out std_logic
    );
end lab12;

architecture arch of lab12 is
    signal logic_ans, arith_ans, final_ans : std_logic_vector(3 downto 0);
    signal overF_sig, neg_sig : std_logic;
    
    component logic_unit
        port(Ain, Bin : in std_logic_vector; F1F0 : in std_logic_vector; Sout : out std_logic_vector);
    end component;
    component arithmetic_unit
        port(Ain, Bin : in std_logic_vector; F1F0 : in std_logic_vector; Sout : out std_logic_vector; Overflow, Negative : out std_logic);
    end component;
    component display
        port(Sin : in std_logic_vector; F2, overF, Negative : in std_logic; a1, b1, c1, d1, e1, f1, g1, g2 : out std_logic);
    end component;
begin
    U0: logic_unit port map(Ain, Bin, F(1 downto 0), logic_ans);
    U1: arithmetic_unit port map(Ain, Bin, F(1 downto 0), arith_ans, overF_sig, neg_sig);
    
    -- MUX21: F(2) 決定輸出 Logic (0) 或 Arithmetic (1)
    final_ans <= arith_ans when F(2) = '1' else logic_ans;
    
    U2: display port map(final_ans, F(2), overF_sig, neg_sig, a1, b1, c1, d1, e1, f1, g1, Neg);
    
    Sout <= final_ans;
    Overflow <= overF_sig and F(2);
end arch;