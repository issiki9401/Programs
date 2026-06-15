library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;

entity OSC is
    port ( clk_in  : in  std_logic;
           clk_out : out std_logic);
end OSC;

architecture behav of OSC is
    signal cnt : std_logic_vector(25 downto 0) := (others => '0');
    signal clk_temp : std_logic := '0';
begin

    process(clk_in) is
    begin
        if(rising_edge(clk_in)) then
            cnt <= cnt + 1;
            if(cnt = 25000000) then  -- 0.5*period 
                cnt <= (others => '0');
                clk_temp <= not clk_temp;
            end if;
        end if;
    end process;

    clk_out <= clk_temp;

end behav;