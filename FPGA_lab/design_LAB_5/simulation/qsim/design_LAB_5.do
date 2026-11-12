onerror {quit -f}
vlib work
vlog -work work design_LAB_5.vo
vlog -work work design_LAB_5.vt
vsim -novopt -c -t 1ps -L cycloneii_ver -L altera_ver -L altera_mf_ver -L 220model_ver -L sgate work.design_LAB_5_vlg_vec_tst
vcd file -direction design_LAB_5.msim.vcd
vcd add -internal design_LAB_5_vlg_vec_tst/*
vcd add -internal design_LAB_5_vlg_vec_tst/i1/*
add wave /*
run -all
