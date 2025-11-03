#include <stdio.h>
int main()
{
 	unsigned int m, n, m_t, n_t;
  	scanf("%u", &m);
  	scanf("%u", &n);
  	m_t = (m/1000)*1000;
  	n_t = (n/1000)*1000;
  	m = m - m_t + n_t;
  	n = n - n_t + m_t;
  	return 0; 
}