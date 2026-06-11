#include <stdio.h>
int main()
{
 	int m, n, tamp;
  	scanf("%d", &m);
  	scanf("%d", &n);
  	tamp = m;
  	m = n;
  	n = tamp;
  	return 0;
}