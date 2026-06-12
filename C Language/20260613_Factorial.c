#include <stdio.h>

#define MOD 1000000007
#define MAX_N 1000

int main() {
    long long fact[MAX_N + 1];
    fact[0] = 1;
    for (int i = 1; i <= MAX_N; i++) {
        fact[i] = (fact[i - 1] * i) % MOD;
    }

    int n;
    while (scanf("%d", &n) != EOF) {
        printf("%lld\n", fact[n]);
    }
    
    return 0;
}