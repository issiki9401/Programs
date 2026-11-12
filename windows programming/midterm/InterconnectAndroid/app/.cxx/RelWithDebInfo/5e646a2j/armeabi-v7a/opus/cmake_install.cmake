# Install script for directory: C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/src/main/cpp/opus

# Set the install prefix
if(NOT DEFINED CMAKE_INSTALL_PREFIX)
  set(CMAKE_INSTALL_PREFIX "C:/Program Files (x86)/interconnect_native")
endif()
string(REGEX REPLACE "/$" "" CMAKE_INSTALL_PREFIX "${CMAKE_INSTALL_PREFIX}")

# Set the install configuration name.
if(NOT DEFINED CMAKE_INSTALL_CONFIG_NAME)
  if(BUILD_TYPE)
    string(REGEX REPLACE "^[^A-Za-z0-9_]+" ""
           CMAKE_INSTALL_CONFIG_NAME "${BUILD_TYPE}")
  else()
    set(CMAKE_INSTALL_CONFIG_NAME "RelWithDebInfo")
  endif()
  message(STATUS "Install configuration: \"${CMAKE_INSTALL_CONFIG_NAME}\"")
endif()

# Set the component getting installed.
if(NOT CMAKE_INSTALL_COMPONENT)
  if(COMPONENT)
    message(STATUS "Install component: \"${COMPONENT}\"")
    set(CMAKE_INSTALL_COMPONENT "${COMPONENT}")
  else()
    set(CMAKE_INSTALL_COMPONENT)
  endif()
endif()

# Install shared libraries without execute permission?
if(NOT DEFINED CMAKE_INSTALL_SO_NO_EXE)
  set(CMAKE_INSTALL_SO_NO_EXE "0")
endif()

# Is this installation the result of a crosscompile?
if(NOT DEFINED CMAKE_CROSSCOMPILING)
  set(CMAKE_CROSSCOMPILING "TRUE")
endif()

# Set default install directory permissions.
if(NOT DEFINED CMAKE_OBJDUMP)
  set(CMAKE_OBJDUMP "C:/Users/1/AppData/Local/Android/Sdk/ndk/27.2.12479018/toolchains/llvm/prebuilt/windows-x86_64/bin/llvm-objdump.exe")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib" TYPE STATIC_LIBRARY FILES "C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/.cxx/RelWithDebInfo/5e646a2j/armeabi-v7a/opus/libopus.a")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/include/opus" TYPE FILE FILES
    "C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/src/main/cpp/opus/include/opus.h"
    "C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/src/main/cpp/opus/include/opus_defines.h"
    "C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/src/main/cpp/opus/include/opus_multistream.h"
    "C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/src/main/cpp/opus/include/opus_projection.h"
    "C:/Users/1/Documents/Github/Programs/windows programming/Midterm/InterconnectAndroid/app/src/main/cpp/opus/include/opus_types.h"
    )
endif()

