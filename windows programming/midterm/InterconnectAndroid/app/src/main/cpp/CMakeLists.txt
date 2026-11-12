cmake_minimum_required(VERSION 3.22.1)
project(interconnect_native C CXX)

set(OPUS_BUILD_SHARED_LIBRARY OFF CACHE BOOL "" FORCE)
set(OPUS_BUILD_PROGRAMS OFF CACHE BOOL "" FORCE)
set(OPUS_BUILD_TESTING OFF CACHE BOOL "" FORCE)
set(OPUS_INSTALL_PKG_CONFIG_MODULE OFF CACHE BOOL "" FORCE)
set(OPUS_INSTALL_CMAKE_CONFIG_MODULE OFF CACHE BOOL "" FORCE)
set(OPUS_ENABLE_FLOAT_API ON CACHE BOOL "" FORCE)
add_subdirectory(opus)

add_library(interconnect_native SHARED opus_jni.c)
target_include_directories(interconnect_native PRIVATE opus/include)
target_link_libraries(interconnect_native opus log aaudio)
