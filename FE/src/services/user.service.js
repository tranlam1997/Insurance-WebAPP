import axios from 'axios';
import authHeader from './auth-header';
const API_URL = 'https://localhost:44312/api/User/';
class UserService {
  getPublicContent() {
    return axios.get(API_URL + 'all');
  }
  getUserInfo() {
    const user = JSON.parse(localStorage.getItem("user"));
    return axios.get(API_URL + user.id, { headers: authHeader() });
  }
  getModeratorBoard() {
    return axios.get(API_URL + 'mod', { headers: authHeader() });
  }
  getAdminBoard() {
    return axios.get(API_URL + 'admin', { headers: authHeader() });
  }
}
export default new UserService();