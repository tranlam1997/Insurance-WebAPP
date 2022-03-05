import axios from 'axios';
import authHeader from './auth-header';
const API_URL = 'https://localhost:44312/api/User/';
class UserService {
  getPublicContent() {
    return axios.get(API_URL + 'all');
  }
  getUserInfo() {
    return  axios.get(API_URL + 'me', { headers: authHeader() });
  }
  editUserInfo(user) {
    return axios.put(API_URL, { firtsName: user.firstName, lastName: user.lastName, dateOfBirth: user.dateOfBirth, phoneNumber: user.phoneNumber, address: user.address}, { headers: authHeader() });
  }
  changeUserPassword(data) {
    return axios.post(API_URL + 'change-password', { password: data.password, newPassword: data.newPassword }, { headers: authHeader() });
  }
  getModeratorBoard() {
    return axios.get(API_URL + 'mod', { headers: authHeader() });
  }
  getAdminBoard() {
    return axios.get(API_URL + 'admin', { headers: authHeader() });
  }
}
export default new UserService();