document.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('token');
    const fullName = localStorage.getItem('fullName');

    if (token) {
        document.getElementById('navLogin').classList.add('d-none');
        document.getElementById('navRegister').classList.add('d-none');
        document.getElementById('navBookings').classList.remove('d-none');
        document.getElementById('navUser').classList.remove('d-none');
        document.getElementById('navLogout').classList.remove('d-none');
        document.getElementById('navUserName').textContent = fullName || 'User';
    }
});

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('fullName');
    localStorage.removeItem('email');
    window.location.href = '/Auth/Login';
}
