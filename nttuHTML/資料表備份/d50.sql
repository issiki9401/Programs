-- phpMyAdmin SQL Dump
-- version 5.2.1deb1+deb12u1
-- https://www.phpmyadmin.net/
--
-- 主機： localhost:3306
-- 產生時間： 2026 年 06 月 15 日 20:33
-- 伺服器版本： 10.11.14-MariaDB-0+deb12u2
-- PHP 版本： 8.2.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫： `d50`
--

-- --------------------------------------------------------

--
-- 資料表結構 `mbr`
--

CREATE TABLE `mbr` (
  `mid` int(11) NOT NULL,
  `uid` varchar(30) NOT NULL,
  `pwd` varchar(30) NOT NULL,
  `username` varchar(30) NOT NULL,
  `reg` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 傾印資料表的資料 `mbr`
--

INSERT INTO `mbr` (`mid`, `uid`, `pwd`, `username`, `reg`) VALUES
(1, 'cat', 'cat2a', '可愛狗', '2026-05-12 15:03:58'),
(2, 'cat', 'cat2a', '可愛狗', '2026-05-12 15:04:22'),
(3, 'f', 'f', '', '2026-05-12 15:42:40'),
(4, 'f', 'f', '', '2026-05-12 15:47:17'),
(5, 'f', 'f', '', '2026-05-12 15:47:47'),
(6, 'f', 'f', '', '2026-05-12 15:54:28'),
(7, 'aa', 'aa', '', '2026-05-12 15:55:31'),
(8, 'aa', 'aa', 'aa', '2026-05-12 15:59:49'),
(9, 'aa', 'aa', '', '2026-05-12 16:09:45'),
(10, 'aa', 'aa', 'f', '2026-05-12 16:10:00'),
(11, 'helloworld', 'abc123', '', '2026-05-12 16:19:52'),
(12, 'helloworld', 'abc123', 'issiki9401', '2026-05-12 16:20:14'),
(13, '', '', '', '2026-05-16 13:18:49');

-- --------------------------------------------------------

--
-- 資料表結構 `menu`
--

CREATE TABLE `menu` (
  `mid` int(11) NOT NULL,
  `mImg` varchar(50) NOT NULL,
  `mName` varchar(50) NOT NULL,
  `mDetail` varchar(100) NOT NULL,
  `mPrice` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 傾印資料表的資料 `menu`
--

INSERT INTO `menu` (`mid`, `mImg`, `mName`, `mDetail`, `mPrice`) VALUES
(1, '../images/m01.png', '四盎司牛肉堡系列', '頂級四盎司安格斯牛肉，厚實多汁大滿足', 79),
(2, '../images/m02.png', '肯瓊辣醬', '多種香料混合，微微辛辣尾韻疊出豐富滋味', 85),
(3, '../images/m03.png', '濃郁吉事片', '濃郁滑順滋味，與牛肉相融完美滋味', 97);

-- --------------------------------------------------------

--
-- 資料表結構 `visitor`
--

CREATE TABLE `visitor` (
  `vid` int(11) NOT NULL,
  `counter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 傾印資料表的資料 `visitor`
--

INSERT INTO `visitor` (`vid`, `counter`) VALUES
(1, 26);

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `mbr`
--
ALTER TABLE `mbr`
  ADD PRIMARY KEY (`mid`);

--
-- 資料表索引 `menu`
--
ALTER TABLE `menu`
  ADD PRIMARY KEY (`mid`);

--
-- 資料表索引 `visitor`
--
ALTER TABLE `visitor`
  ADD PRIMARY KEY (`vid`);

--
-- 在傾印的資料表使用自動遞增(AUTO_INCREMENT)
--

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `mbr`
--
ALTER TABLE `mbr`
  MODIFY `mid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `menu`
--
ALTER TABLE `menu`
  MODIFY `mid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `visitor`
--
ALTER TABLE `visitor`
  MODIFY `vid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
