@echo off
"C:\\Users\\1\\AppData\\Local\\Android\\Sdk\\cmake\\3.22.1\\bin\\cmake.exe" ^
  "-HC:\\Users\\1\\Documents\\Github\\Programs\\windows programming\\Midterm\\InterconnectAndroid\\app\\src\\main\\cpp" ^
  "-DCMAKE_SYSTEM_NAME=Android" ^
  "-DCMAKE_EXPORT_COMPILE_COMMANDS=ON" ^
  "-DCMAKE_SYSTEM_VERSION=26" ^
  "-DANDROID_PLATFORM=android-26" ^
  "-DANDROID_ABI=arm64-v8a" ^
  "-DCMAKE_ANDROID_ARCH_ABI=arm64-v8a" ^
  "-DANDROID_NDK=C:\\Users\\1\\AppData\\Local\\Android\\Sdk\\ndk\\27.2.12479018" ^
  "-DCMAKE_ANDROID_NDK=C:\\Users\\1\\AppData\\Local\\Android\\Sdk\\ndk\\27.2.12479018" ^
  "-DCMAKE_TOOLCHAIN_FILE=C:\\Users\\1\\AppData\\Local\\Android\\Sdk\\ndk\\27.2.12479018\\build\\cmake\\android.toolchain.cmake" ^
  "-DCMAKE_MAKE_PROGRAM=C:\\Users\\1\\AppData\\Local\\Android\\Sdk\\cmake\\3.22.1\\bin\\ninja.exe" ^
  "-DCMAKE_LIBRARY_OUTPUT_DIRECTORY=C:\\Users\\1\\Documents\\Github\\Programs\\windows programming\\Midterm\\InterconnectAndroid\\app\\build\\intermediates\\cxx\\RelWithDebInfo\\5e646a2j\\obj\\arm64-v8a" ^
  "-DCMAKE_RUNTIME_OUTPUT_DIRECTORY=C:\\Users\\1\\Documents\\Github\\Programs\\windows programming\\Midterm\\InterconnectAndroid\\app\\build\\intermediates\\cxx\\RelWithDebInfo\\5e646a2j\\obj\\arm64-v8a" ^
  "-DCMAKE_BUILD_TYPE=RelWithDebInfo" ^
  "-BC:\\Users\\1\\Documents\\Github\\Programs\\windows programming\\Midterm\\InterconnectAndroid\\app\\.cxx\\RelWithDebInfo\\5e646a2j\\arm64-v8a" ^
  -GNinja ^
  "-DANDROID_STL=c++_static"
