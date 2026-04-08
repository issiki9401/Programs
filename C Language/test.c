#include <stdio.h>
#include <string.h>

char* findlastCity(char *cities[], int size){
    if(size <= 0){
        return NULL; 
    }
    char *lastCity = cities[0];
    for(int i = 1; i < size; i++){
        if(strcmp(cities[i], lastCity) > 0){
            lastCity = cities[i];
        }
    }
    return lastCity;
}

int main(){
    char *cities[ ] = {"London", "New York", "Tokyo", "Paris", "Taipei"};
    int size = sizeof(cities) / sizeof(cities[0]);
    char *result = findlastCity(cities, size);
    if(result != NULL){
        printf(result);
    }
    else{
        printf("Can't find the city\n");
    }

    return 0;
}