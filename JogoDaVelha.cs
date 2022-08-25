namespace JogoDaVelha{
    class JogoDaVelha{
        private bool fimDeJogo;
        private char[] posicoes;
        private char jogadorRodada;
        private int jogadasRealizadas;
        private String nomeJogador;
        private char jogarNovamente;

        //inicializa valores utilizados
       public JogoDaVelha(){
            fimDeJogo = false;
            jogarNovamente = 'N';
            posicoes = new []{'1', '2', '3', '4', '5', '6', '7', '8', '9'};
            jogadorRodada = 'X';
            jogadasRealizadas = 0;
            nomeJogador = "jogador1 (X)";
        }

        public void Iniciar(){

            //looping de rodadas até obter: 1 jogador ganhador ou 1 empate, validando variável fimDeJogo
            while(!fimDeJogo){
                GerarTabela();
                LerEscolhaJogador();
                GerarTabela();
                VerificarFimJogo();
                AlterarJogador();
            }

            //confirma se usuario deseja jogar novamente após sair do looping de rodadas
            Console.WriteLine("Deseja jogar novamente? Digite 'S' para repetir ou qualquer outra tecla para finalizar. Obrigado :) \n");
            jogarNovamente = Console.ReadKey().KeyChar;
            if(jogarNovamente == 'S' || jogarNovamente == 's'){
                Console.Clear();
                fimDeJogo = false;  
                ResetarJogo();    
                Iniciar();          
            } else {
                Console.WriteLine("\n\n O jogo foi finalizado, volte quando quiser! \n");
            }

        }
            //limpa e inicializa o tabuleiro - tabela
            private void GerarTabela(){
                Console.Clear();
                //mensagem de boas vindas
                Console.WriteLine("\n\n Bem vind(x)s ao Jogo da Velha Moderno, para jogar é bem simples, digíte um número entre 1 a 9 para preencher a posição \n desejada. Boa sorte!");
                Console.WriteLine(RenderizarTabuleiro());
            }   

            private void LerEscolhaJogador(){
                
                //para maior clareza, definido o nome extenso do jogador da vez
                if(jogadorRodada == 'O'){ 
                    nomeJogador = "Jogador2 (O)";
                } else {
                    nomeJogador = "Jogador1 (X)";
                }

                Console.WriteLine($"Agora é a vez de { nomeJogador }, digite um número entre 1 a 9 para preencher uma posição no tabuleiro.");
                //valida se valor é inteiro - se pode ser convertido                                  //valida se valor esta entre 0 e 9
                bool conversaoValida = int.TryParse(Console.ReadLine(), out int posicaoEscolhida) && (posicaoEscolhida <= 9 && posicaoEscolhida > 0);

                //looping validacao para ambos os casos
                while(!conversaoValida || !ValidarEscolhaJogador(posicaoEscolhida)){
                    Console.WriteLine("O valor informado é inválido, digite um número entre 1 e 9 para preencher uma posição no tabuleiro.");
                    conversaoValida = int.TryParse(Console.ReadLine(), out posicaoEscolhida) && (posicaoEscolhida <= 9 && posicaoEscolhida > 0);
                }

                PreencherEscolha(posicaoEscolhida);
            }

            //reseta as variáveis do jogo pare iniciar outra partida
            private void ResetarJogo(){
                
                fimDeJogo = false;
                jogarNovamente = 'N';
                posicoes = new []{'1', '2', '3', '4', '5', '6', '7', '8', '9'};
                jogadorRodada = 'X';
                jogadasRealizadas = 0;
                nomeJogador = "jogador1 (X)";
            }
            
            //setar escolha do jogador e contabilizar jogadas
            private void PreencherEscolha(int posicaoEscolhida){
                int indice = posicaoEscolhida - 1;
                posicoes[indice] = jogadorRodada;
                jogadasRealizadas++;
            }

            //validacao da posicao escolhida
            private bool ValidarEscolhaJogador(int posicaoEscolhida){
                //valor digitado -1 devido as posicoes do array
                int indice = posicaoEscolhida - 1;
                return (posicoes[indice] != 'O' && posicoes[indice] != 'X');
                //(posicaoEscolhida >= 1 && posicaoEscolhida <= 9)); 
            }

            //verificar se exite 3 valores iguais em linha preenchido por um jogador
            private void VerificarFimJogo(){
                //so é possivel um ganhador apos 5º jogada
                if(jogadasRealizadas >= 5 && (ObservarHorizontal() || ObservarVertical() || ObservarDiagonal()) ){
                    fimDeJogo = true;
                    Console.WriteLine($"Fim de Jogo!! O jogador {nomeJogador} ganhou!");
                    return;
                } 
                //se tabuleiro completo considera-se empate
                if(jogadasRealizadas is 9){
                    fimDeJogo = true;
                    Console.WriteLine("\n O jogo terminou empatado!!");
                }
            }

            //observar se existe 3 valores iguais em linhas horizontais
            private bool ObservarHorizontal(){
                bool ganhouLinha1 = posicoes[0] ==  posicoes[1] &&  posicoes[0] ==  posicoes[2]; 
                bool ganhouLinha2 = posicoes[3] ==  posicoes[4] &&  posicoes[3] ==  posicoes[5]; 
                bool ganhouLinha3 = posicoes[6] ==  posicoes[7] &&  posicoes[6] ==  posicoes[8]; 

                return (ganhouLinha1 || ganhouLinha2 || ganhouLinha3);
            }

            //observar se existe 3 valores iguais em linhas verticais
            private bool ObservarVertical(){
                bool ganhouLinha1 = posicoes[0] ==  posicoes[3] &&  posicoes[0] ==  posicoes[6]; 
                bool ganhouLinha2 = posicoes[1] ==  posicoes[4] &&  posicoes[1] ==  posicoes[7]; 
                bool ganhouLinha3 = posicoes[2] ==  posicoes[5] &&  posicoes[2] ==  posicoes[8]; 

                return (ganhouLinha1 || ganhouLinha2 || ganhouLinha3);
            }

            //observar se existe 3 valores iguais em linhas diagonáis
            private bool ObservarDiagonal(){
                bool ganhouLinha1 = posicoes[2] ==  posicoes[4] &&  posicoes[2] ==  posicoes[6]; 
                bool ganhouLinha2 = posicoes[0] ==  posicoes[4] &&  posicoes[0] ==  posicoes[8]; 

                return (ganhouLinha1 || ganhouLinha2);
            }

            //alternar jogador atual
            private void AlterarJogador(){ 
                if(jogadorRodada == 'X'){
                    jogadorRodada = 'O';
                } else {
                    jogadorRodada = 'X';
                }
            }       
            
            //gerar a visualizacao do jogo - tabuleiro
            private string RenderizarTabuleiro(){

                return  "\n     |     |      \n" +
                        $"  {posicoes[0]}  |  {posicoes[1]}  |  {posicoes[2]}\n" +
                        "_____|_____|_____ \n" +
                        "     |     |      \n" +
                        $"  {posicoes[3]}  |  {posicoes[4]}  |  {posicoes[5]}\n" +
                        "_____|_____|_____ \n" +
                        "     |     |      \n" +
                        $"  {posicoes[6]}  |  {posicoes[7]}  |  {posicoes[8]}\n" +
                        "     |     |      \n\n";
            }
            //obs: tentei deixar o código o mais claro e funcional, sem utilizar de operadores ternarios, por exemplo. 
            //e com uma validacao adequada para valores fora do indicado ao usuario. Espero que gostem :)
    }
}