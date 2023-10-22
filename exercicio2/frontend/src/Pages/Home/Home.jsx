import Card from "../../Components/Card";
import "./Home.css";

import { useEffect, useState } from "react";

const Home = () => {
  const [usuarios, setUsuarios] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("http://localhost:5248/usuario")
      .then((res) => res.json())
      .then((data) => {
        setUsuarios(data);
        setLoading(false);
      })
      .catch((err) => {
        console.log(err);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <p>Carregando...</p>;
  }

  return (
    <div className="container">
      <h1>Usuários</h1>

      <div className="cards">
        {usuarios.map((user) => (
          <Card usuario={user} key={user.id} />
        ))}
      </div>
    </div>
  );
};

export default Home;
