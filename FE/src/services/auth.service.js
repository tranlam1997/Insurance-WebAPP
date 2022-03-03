import axios from 'axios';
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
          localStorage.setItem('user', JSON.stringify({...response.data, email: user.email, password: user.password}));
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