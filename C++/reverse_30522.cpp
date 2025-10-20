#include <string.h>  
#include<iostream>  

using namespace std;  
int main()  
{  
    int i,ilength;  
    char str1[20];  
    cout << "輸入一個字串: ";  
    cin >> str1;  
    ilength = strlen(str1);  
    cout << "該字串的反向:";  
    for(i = ilength;i>=1;i--){  
        cout << str1[i];  
    }  
    cout << str1[0] << endl;  
}