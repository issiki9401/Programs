#include <stdio.h>
int main()
{
 	unsigned int m, n, k1, k2;
  	scanf("%u", &m);
  	scanf("%u", &n);
  	k1 = m/n;
  	k2 = m%n;
 	printf("%u = %u * %u + %u\n", m, n, k1, k2);
  	return 0;
}