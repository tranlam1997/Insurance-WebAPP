import axios from 'axios';
import jwt_decode from 'jwt-decode';
const API_URL = 'https://localhost:44312/api/User/';

class AuthService {
  login(user) {
    return axios
      .post(API_URL + 'login', {
        email: user.email,
        password: user.password
      })
      .then(response => {
        if (response.data.token) {
          const decoded = jwt_decode(response.data.token);
          const userId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
          localStorage.setItem('user', JSON.stringify({...response.data, id: userId, email: user.email, password: user.password}));
        }
        return response.data;
      });
  }
  logout() {
    localStorage.removeItem('user');
  }
  register(user) {
    return axios.post(API_URL + 'register', {
      firtsName: user.firstName,
      lastName: user.lastName,
      dateOfBirth: user.dateOfBirth,
      email: user.email,
      address: user.address,
      phoneNumber: user.phoneNumber,
      password: user.password,
    });
  }
}
export default new AuthService();