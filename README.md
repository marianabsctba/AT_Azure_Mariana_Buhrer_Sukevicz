
** Link do Azure:  https://app-final-at.azurewebsites.net/

**Observações: acabei não conseguindo publicar no serveless (dava um erro...), então está com um pouco de delay (talvez tenha sido a escolha de servidor), embora esteja funcionando. 
Assim, ao criar nova doação, quando acessar os detalhes, usar F5 para atualizar a página de detalhes. Qualquer dúvida, inseri um vídeo MP4 com o funcionamento do código.



**Enunciado: 

Na disciplina de Desenvolvimento com Azure, você compreendeu as tecnologias e principais serviços disponíveis na nuvem da Microsoft que te habilitam a realizar as seguintes ações: publicar projetos web, armazenar arquivos utilizando blobs, enviar mensagens utilizando filas, persistir dados em banco SQL, disparar funções serverless.

Nesse Assessment, você deverá criar uma aplicação web (MVC) usando todos esses principais serviços que você aprendeu nessa disciplina.

Você deve criar um projeto do tipo ASP.NET Core Web MVC. Nesse projeto você deve desenvolver um CRUD básico (listagem, detalhe, inclusão, alteração, exclusão) cuja entidade tenha pelo menos cinco atributos diferentes (incluindo o endereço web de uma imagem). As operações do CRUD devem persistir os dados da entidade em um banco de dados SQL do Azure (TP2).

Nas operações de CRUD, a aplicação deve ser capaz de armazenar a imagem como Blob no Azure. Além da inclusão, a aplicação deve ser capaz de alterar e excluir o Blob nas operações de alteração e exclusão respectivamente. A exibição da imagem deve ser feita no navegador na página de detalhe (TP2).

Na operação de visualização da entidade (Detalhe), a aplicação deve ser capaz de exibir um campo numérico com a quantidade de vezes que a página de detalhe foi visualizada. (TP3)

Desenvolva uma Azure Function com disparo (trigger) a partir de uma fila. Dentro da Azure Function faça uma conexão com o banco de dados e incremente (+ 1) a quantidade de visualizações na tabela correspondente a entidade. Persista uma mensagem em fila (Queue) toda vez que o usuário navegar para a página de detalhe, de modo que a Function seja disparada.

Configure o Application Insights na aplicação Web Mvc para monitoramento da aplicação.

Adicionalmente, a aplicação desenvolvida deve conter projetos e classes separados por camadas (apresentação, negócio e dados).

