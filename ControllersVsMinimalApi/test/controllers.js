import http from 'k6/http';

export const options = {
    duration: '60s',
};
export default function () {
    http.get('http://localhost:5000/controllers/users/1');
}