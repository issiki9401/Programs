//輸入輸出
#include <stdio.h>
//Heap用
#include <stdlib.h>
//tolower用
#include <ctype.h>

int calculate(const char *traget);
int is_vowel(char c);
char* remove_vowels(const char *target);

//可用<string.h>的strlen代替
//算字串長度的函式
int calculate(const char *target){

    int len=0;
    while (target[len] != '\0'){
    len++;
    }

    return len;
}

//算母音數量的函式
int is_vowel(char a) {
    a=tolower(a);
    return (a=='a' || a=='e' || a=='i' || a=='o' || a=='u');
}

//移除母音的函式
char* remove_vowels(const char *target){
    //算字串長度
    int len=calculate(target);
    int count=0;

    //算母音數量
    for (int i = 0; i < len; i++) {
        if (!is_vowel(target[i])) {
            count++;
        }
    }

    //申請空間
    char *result = (char*)malloc((count + 1) * sizeof(char));
    if (result == NULL) {
        return NULL; //防止當機
    }

    //搬家
    int j = 0;
    for (int i = 0; i < len; i++) {
        if (!is_vowel(target[i])) {
            result[j] = target[i];
            j++;
        }
    }
    result[j] = '\0';

    return result;
}

//主函式
int main(){
    char target[100];

    //輸入字串
    fgets(target, sizeof(target), stdin);
    
    //導入函式
    char *result_str = remove_vowels(target);

    //搬過家的字串+印出結果
    if (result_str != NULL) {
        printf("%s\n", result_str);

        free(result_str); 
        result_str = NULL;
        }
    
    return 0;
}