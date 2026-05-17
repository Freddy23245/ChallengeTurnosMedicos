<template>
  <div>
    <nav class="navbar">
      <span class="brand">Turnos Médicos</span>
      <div class="links">
        <router-link to="/turnos">Turnos</router-link>
        <router-link to="/turnos/nuevo">Nuevo turno</router-link>
        <router-link to="/pacientes">Pacientes</router-link>
      </div>
    </nav>
    <main class="main-content">
      <router-view />
    </main>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 24px;
  height: 56px;
  background: #1a73e8;
  color: #fff;
}
.brand {
  font-weight: 600;
  font-size: 18px;
}
.links a {
  color: #fff;
  text-decoration: none;
  margin-left: 24px;
  font-size: 14px;
  opacity: 0.85;
}
.links a.router-link-active {
  opacity: 1;
  font-weight: 600;
  border-bottom: 2px solid #fff;
  padding-bottom: 2px;
}
.main-content {
  padding: 32px 24px;
  max-width: 1100px;
  margin: 0 auto;
}
table {
  width: 100%;
  border-collapse: collapse;
  background: #fff;
  border-radius: 6px;
  overflow: hidden;
  box-shadow: 0 1px 4px rgba(0,0,0,.08);
}
th, td {
  padding: 12px 16px;
  text-align: left;
  font-size: 14px;
}
th {
  background: #f0f4f8;
  font-weight: 600;
  color: #555;
}
tr:not(:last-child) td {
  border-bottom: 1px solid #eee;
}
button {
  padding: 6px 14px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 13px;
  background: #1a73e8;
  color: #fff;
}
button:hover { background: #1558b0; }
.btn-danger { background: #d32f2f; }
.btn-danger:hover { background: #b71c1c; }
.form-group {
  margin-bottom: 18px;
}
.form-group label {
  display: block;
  font-size: 13px;
  font-weight: 600;
  margin-bottom: 6px;
  color: #555;
}
.form-group input,
.form-group select {
  width: 100%;
  padding: 9px 12px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}
.card {
  background: #fff;
  border-radius: 6px;
  padding: 24px;
  box-shadow: 0 1px 4px rgba(0,0,0,.08);
}
h2 {
  margin-bottom: 20px;
  font-size: 20px;
}
</style>
