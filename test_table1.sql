/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : MySQL
 Source Server Version : 80018
 Source Host           : localhost:3306
 Source Schema         : mysql

 Target Server Type    : MySQL
 Target Server Version : 80018
 File Encoding         : 65001

 Date: 04/05/2020 08:32:02
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for test_table1
-- ----------------------------
DROP TABLE IF EXISTS `test_table1`;
CREATE TABLE `test_table1`  (
  `id` int(4) NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `birthday` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of test_table1
-- ----------------------------
INSERT INTO `test_table1` VALUES (1, 't11-name12', NULL);
INSERT INTO `test_table1` VALUES (2, 'n2-nAME222', NULL);
INSERT INTO `test_table1` VALUES (3, 'name3', NULL);

SET FOREIGN_KEY_CHECKS = 1;
