#include <stdio.h>

int main() {
    unsigned int m;
    
    scanf("%u", &m);
    
    int thousand_digit = (m / 1000) % 10;
    int hundred_digit = (m / 100) % 10;
    
    m = m - (thousand_digit * 1000) - (hundred_digit * 100) + (hundred_digit * 1000) + (thousand_digit * 100);
 
    printf("%d\n", m);
    
    return 0;
}