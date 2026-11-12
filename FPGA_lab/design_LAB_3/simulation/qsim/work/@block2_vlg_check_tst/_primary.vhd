library verilog;
use verilog.vl_types.all;
entity Block2_vlg_check_tst is
    port(
        Cout            : in     vl_logic;
        S0              : in     vl_logic;
        S1              : in     vl_logic;
        S2              : in     vl_logic;
        sampler_rx      : in     vl_logic
    );
end Block2_vlg_check_tst;
