import { BrowserRouter, Route, Routes } from "react-router-dom";

import Home from "./Pages/Home/Home";
import Pagamento from "./Pages/Pagamento/Pagamento";
import NotFound from "./Pages/NotFound/NotFound";

function App() {
  return (
    <>
      {/* Rotas do projeto, qualquer rota errada irá cair no NotFound */}
      {/* Se tentar colocar um id não existente na mão irá retornar para o Home */}
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/pagamento/:id" element={<Pagamento />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
