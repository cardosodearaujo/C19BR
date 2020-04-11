using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

public class Tools
{
    private string[] dadosArray = new[] { "000.000.000-00", "111.111.111-11", "222.222.222-22", "333.333.333-33", "444.444.444-44", "555.555.555-55", "666.666.666-66", "777.777.777-77", "888.888.888-88", "999.999.999-99" };

    private const string msgErro = "Dados Inválidos";
    private bool bValida;

    public static string Formatar(string valor, string mascara)
    {
        StringBuilder dado = new StringBuilder();
        foreach (char c in valor)
        {
            if (Char.IsNumber(c))
                dado.Append(c);
        }

        int indMascara = mascara.Length;
        int indCampo = dado.Length;

        for (; indCampo > 0 && indMascara > 0;)
        {
            if (mascara[--indMascara] == '#')
                indCampo--;
        }

        StringBuilder saida = new StringBuilder();
        for (; indMascara < mascara.Length; indMascara++)
        {
            saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);
        }
        return saida.ToString();
    }

    public static string RemoveCaracteresEspeciais(string texto, bool aceitaEspaco, bool substituiAcentos)
    {
        string vtexto = texto;
        string ret = string.Empty;

        if (substituiAcentos)
            vtexto = RemoveAcentos(texto);

        if (aceitaEspaco)
            ret = System.Text.RegularExpressions.Regex.Replace(vtexto, @"(?i)[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", string.Empty);
        else
            ret = System.Text.RegularExpressions.Regex.Replace(vtexto, @"(?i)[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);

        return ret;
    }

    public static string RemoveAcentos(string text)
    {
        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }
        return sbReturn.ToString();
    }

    //public string MascaraValor(object Valor, string TIPO)
    //{
    //    int TamVLR, X, TamX;
    //    string SaidaVLR = "";
    //    TamVLR = Valor.ToString().Count;
    //    TamX = TamVLR;
    //    switch (TIPO)
    //    {
    //        case "Milhar":
    //            {
    //                for (X = 1; X <= TamVLR; X++)
    //                {
    //                    SaidaVLR = Strings.Mid(Valor.ToString(), TamX, 1) + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 3)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 7)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 11)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 15)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    TamX = TamX - 1;
    //                }
    //                if (Strings.Left(SaidaVLR, 1) == ".")
    //                    SaidaVLR = Strings.Right(SaidaVLR, (Strings.Len(SaidaVLR) - 1));
    //                break;
    //            }

    //        case "DuasCasasDecimais":
    //            {
    //                for (X = 1; X <= TamVLR; X++)
    //                {
    //                    SaidaVLR = Strings.Mid(Valor.ToString(), TamX, 1) + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 2)
    //                        SaidaVLR = "," + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 6)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 10)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 14)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    TamX = TamX - 1;
    //                }
    //                if (Strings.Left(SaidaVLR, 1) == "." | Strings.Left(SaidaVLR, 1) == ",")
    //                    SaidaVLR = Strings.Right(SaidaVLR, (Strings.Len(SaidaVLR) - 1));
    //                break;
    //            }

    //        case "TresCasasDecimais":
    //            {
    //                for (X = 1; X <= TamVLR; X++)
    //                {
    //                    SaidaVLR = Strings.Mid(Valor.ToString(), TamX, 1) + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 3)
    //                        SaidaVLR = "," + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 7)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 11)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    if (Strings.Len(SaidaVLR) == 15)
    //                        SaidaVLR = "." + SaidaVLR;
    //                    TamX = TamX - 1;
    //                }
    //                if (Strings.Left(SaidaVLR, 1) == "." | Strings.Left(SaidaVLR, 1) == ",")
    //                    SaidaVLR = Strings.Right(SaidaVLR, (Strings.Len(SaidaVLR) - 1));
    //                break;
    //            }
    //    }
    //    return SaidaVLR;
    //}

    public string MASCARA(object CAMPO, string TIPO)
    {
        string VALORATUAL = CAMPO.ToString();
        string VALORNUMERICO = "";
        int NINDEXMODELO = 0;
        int NINDEXSTRING = 0;
        string VALORFINAL = "";
        bool ADICIONARVALOR = true;
        string MODELO = "";
        string TAMANHO;
        switch (TIPO)
        {
            case "TCPIP4":
                {
                    MODELO = "###.###.###.###";
                    break;
                }

            case "NCM":
                {
                    MODELO = "#####.##.##";
                    break;
                }

            case "CNPJ":
                {
                    MODELO = "##.###.###/####-##";
                    break;
                }

            case "CARTAO":
                {
                    MODELO = "####-####-####-####-####";
                    break;
                }

            case "TELEFONE":
                {
                    MODELO = "(##) ####-####";
                    break;
                }

            case "CELULAR":
                {
                    MODELO = "(##) #####-####";
                    break;
                }

            case "DATA":
                {
                    MODELO = "##/##/####";
                    break;
                }

            case "CPF":
                {
                    MODELO = "###.###.###-##";
                    break;
                }

            case "CGC":
                {
                    MODELO = "##.###.###/####-##";
                    break;
                }

            case "IE":
                {
                    MODELO = "###.###.###";
                    break;
                }

            case "CEP":
                {
                    MODELO = "#####-###";
                    break;
                }

            case "RG":
                {
                    TAMANHO = VALORATUAL;
                    if ((TAMANHO.Length >= 4))
                    {
                        TAMANHO = TAMANHO.Replace(".", "");
                        TAMANHO = TAMANHO.Replace(".", "");
                        TAMANHO = TAMANHO.Replace(".", "");
                        TAMANHO = TAMANHO.Replace(".", "");
                        TAMANHO = TAMANHO.Replace(".", "");
                    }
                    var vTAMANHO = TAMANHO.Length;
                    switch (vTAMANHO)
                    {
                        case 1:
                            {
                                MODELO = "#";
                                break;
                            }

                        case 2:
                            {
                                MODELO = "##";
                                break;
                            }

                        case 3:
                            {
                                MODELO = "###";
                                break;
                            }

                        case 4:
                            {
                                MODELO = "#.###";
                                break;
                            }

                        case 5:
                            {
                                MODELO = "##.###";
                                break;
                            }

                        case 6:
                            {
                                MODELO = "###.###";
                                break;
                            }

                        case 7:
                            {
                                MODELO = "#.###.###";
                                break;
                            }

                        case 8:
                            {
                                MODELO = "##.###.###";
                                break;
                            }

                        case 9:
                            {
                                MODELO = "###.###.###";
                                break;
                            }

                        case 10:
                            {
                                MODELO = "#.###.###.###";
                                break;
                            }

                        case 11:
                            {
                                MODELO = "##.###.###.###";
                                break;
                            }

                        case 12:
                            {
                                MODELO = "###.###.###.###";
                                break;
                            }

                        case 13:
                            {
                                MODELO = "#.###.###.###.###";
                                break;
                            }

                        case 14:
                            {
                                MODELO = "##.###.###.###.###";
                                break;
                            }

                        case 15:
                            {
                                MODELO = "###.###.###.###.###";
                                break;
                            }
                    }

                    break;
                }
        }

        for (int I = 0; I == MODELO.Length; I++)
        {
            if ((MODELO.Substring(I, 1) != "#"))
                VALORATUAL = VALORATUAL.Replace(MODELO.Substring(I, 1), "");
        }
                
        for (int I = 0; I == VALORATUAL.Length; I++)
        {
            if (double.IsNaN(double.Parse(VALORATUAL.Substring(I, 1))))
                VALORNUMERICO = VALORNUMERICO + VALORATUAL.Substring(I, 1);
        }

        for (int I = 0;I == MODELO.Length; I++)
        {
            if (MODELO.Substring(I, 1) == "#")
            {
                if (VALORNUMERICO.Substring(NINDEXMODELO, 1) != "")
                {
                    NINDEXMODELO = NINDEXMODELO + 1;
                    NINDEXSTRING = NINDEXSTRING + 1;
                }
                else
                    ADICIONARVALOR = false;
            }
            else if (ADICIONARVALOR & VALORNUMERICO.Substring(NINDEXMODELO, 1) != "")
            {
                VALORFINAL = VALORATUAL + MODELO.Substring(NINDEXSTRING, 1);
                NINDEXSTRING = NINDEXSTRING + 1;
            }
        }

        return VALORATUAL;
    }

    public string Criptog(string VarEntrada)
    {
        string VarSaida = "";
        string VarTmp = ""; 
        VarTmp = VarEntrada.Trim();
        VarTmp = (string)VarTmp.Reverse(); 
        int i = 0; 
        string Asc_Caracter; 
        for (i = 1; i <= VarTmp.Length; i++) 
        {
            Asc_Caracter =  (string)(VarTmp.Substring(i, 1)).ToString(); 
            if (int.Parse(Asc_Caracter) < 100)
                Asc_Caracter = string.Empty.PadRight(3 - Asc_Caracter.Length, '0') + Asc_Caracter;
            VarSaida = VarSaida + Asc_Caracter;
        }

        return VarSaida;
    }

    public string DsCriptog(string VarEntrada)
    {
        string VarSaida = "";
        string VarTmp = ""; 
        VarTmp = VarEntrada.Trim();
        int i = 0;
        string Asc_Caracter; 
        i = 1;
        if (VarEntrada != "")
        {
            while (i <= VarTmp.Length)
            {
                Asc_Caracter = VarTmp.Substring(i, 3); 
                Asc_Caracter = Convert.ToInt32(Asc_Caracter).ToString(); 
                VarSaida = VarSaida + Asc_Caracter;
                i = i + 3;
            }
            VarSaida = (string)VarSaida.Reverse();
        }
        return VarSaida;
    }

    public void LimpaArrayList(ref ArrayList listaVetor)
    {
        var i = 0;
        while (i <= listaVetor.Count - 1)
        {
            if (listaVetor[i] == null)
            {
                listaVetor.RemoveAt(i);
                i = i - 1;
            }
            i = i + 1;
        }

        if (listaVetor.Count > 0)
        {
            listaVetor.Sort();

            var anterior = (Convert.ToInt32(listaVetor[0]));

            i = 1;
            while (i <= listaVetor.Count - 1)
            {
                var atual = Convert.ToInt32(listaVetor[i]);

                if (atual == anterior)
                {
                    listaVetor.RemoveAt(i);
                    i = i - 1;
                }
                else
                    anterior = atual;
                i = i + 1;
            }
        }
    }

    public string GeraCondional(ArrayList listaVetor)
    {
        System.Text.StringBuilder sbItem = new System.Text.StringBuilder();
        string stItem = string.Empty;

        for (int i = 0; i <= listaVetor.Count - 1; i++)
        {
            if (listaVetor[i] == null)
                i = i + 1;

            sbItem.Append(Convert.ToString(listaVetor[i]));

            if (listaVetor.Count - 1 != i)
                sbItem.Append(", ");
        }
        stItem = sbItem.ToString();

        if (stItem != string.Empty)
            return stItem;
        else
            return "0";
    }

    //public string FormataTelefone(string srFONE)
    //{
    //    string fFONE = string.Empty;
    //    int iTAM;
    //    int ix;
    //    string strCHAR = string.Empty;
    //    ix = 1;
    //    iTAM = Strings.Len(srFONE);
    //    for (ix = 1; ix <= iTAM; ix++)
    //    {
    //        strCHAR = Strings.Mid(srFONE, ix, 1);
    //        if (strCHAR == "0" | strCHAR == "1" | strCHAR == "2" | strCHAR == "3" | strCHAR == "4" | strCHAR == "5" | strCHAR == "6" | strCHAR == "7" | strCHAR == "8" | strCHAR == "9")
    //            fFONE = fFONE + strCHAR;
    //    }

    //    srFONE = fFONE;
    //    iTAM = Strings.Len(srFONE);

    //    if (iTAM > 0)
    //    {
    //        switch (iTAM)
    //        {
    //            case 7:
    //                {
    //                    fFONE = Strings.Left(srFONE, 3) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            case 8:
    //                {
    //                    fFONE = Strings.Left(srFONE, 4) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            case 9:
    //                {
    //                    fFONE = "(" + Strings.Left(srFONE, 2) + ")" + Strings.Mid(srFONE, 3, 3) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            case 10:
    //                {
    //                    fFONE = "(" + Strings.Left(srFONE, 2) + ")" + Strings.Mid(srFONE, 3, 4) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            case 11:
    //                {
    //                    fFONE = "(" + Strings.Left(srFONE, 2) + ")" + Strings.Mid(srFONE, 3, 5) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            default:
    //                {
    //                    fFONE = srFONE;
    //                    break;
    //                }
    //        }
    //    }

    //    srFONE = fFONE;
    //    return srFONE;
    //}

    //public string FormataCelular(string srFONE)
    //{
    //    string fFONE = string.Empty;
    //    int iTAM;
    //    int ix;
    //    string strCHAR = string.Empty;
    //    ix = 1;
    //    iTAM = Strings.Len(srFONE);
    //    for (ix = 1; ix <= iTAM; ix++)
    //    {
    //        strCHAR = Strings.Mid(srFONE, ix, 1);
    //        if (strCHAR == "0" | strCHAR == "1" | strCHAR == "2" | strCHAR == "3" | strCHAR == "4" | strCHAR == "5" | strCHAR == "6" | strCHAR == "7" | strCHAR == "8" | strCHAR == "9")
    //            fFONE = fFONE + strCHAR;
    //    }

    //    srFONE = fFONE;
    //    iTAM = Strings.Len(srFONE);

    //    if (iTAM > 0)
    //    {
    //        switch (iTAM)
    //        {
    //            case 9:
    //                {
    //                    fFONE = Strings.Left(srFONE, 5) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            case 10:
    //                {
    //                    fFONE = "(" + Strings.Left(srFONE, 2) + ")" + Strings.Mid(srFONE, 3, 4) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            case 11:
    //                {
    //                    fFONE = "(" + Strings.Left(srFONE, 2) + ")" + Strings.Mid(srFONE, 3, 5) + "-" + Strings.Right(srFONE, 4);
    //                    break;
    //                }

    //            default:
    //                {
    //                    fFONE = srFONE;
    //                    break;
    //                }
    //        }
    //    }

    //    srFONE = fFONE;
    //    return srFONE;
    //}

    public bool EnvioDeEmailSemAnexos(SmtpClient objSmtp, MailAddress De, string Para, string ComCopia, string ComCopiaOculta, string Assunto, string Corpo, System.Text.Encoding CodificaoDoCorpo, MailPriority Prioridade, AlternateView vw = null)
    {
        MailMessage ClasseDeEmail = new MailMessage();
        try
        {
            {
                var withBlock = ClasseDeEmail;
                withBlock.From = De;
                if (Para != string.Empty)
                    withBlock.To.Add(Para);
                if (ComCopia != string.Empty)
                    withBlock.CC.Add(ComCopia);
                if (ComCopiaOculta != string.Empty)
                    withBlock.Bcc.Add(ComCopiaOculta);
                withBlock.Subject = Assunto;
                withBlock.IsBodyHtml = true;
                withBlock.Body = Corpo;
                withBlock.BodyEncoding = CodificaoDoCorpo;
                withBlock.Priority = Prioridade;
            }
            try
            {
                objSmtp.Send(ClasseDeEmail);
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
        catch (Exception ex)
        {
            throw;
            return false;
        }
        finally
        {
            ClasseDeEmail.Dispose();
        }
    }

    public bool EnvioDeEmailComAnexos(SmtpClient objSmtp, MailAddress De, string Para, string ComCopia, string ComCopiaOculta, string Assunto, string Corpo, System.Text.Encoding CodificaoDoCorpo, MailPriority Prioridade, List<string> Anexos, byte[] Logo = null, string WidthPadrao = "")
    {
        MailMessage ClasseDeEmail = new MailMessage();
        try
        {
            {
                var withBlock = ClasseDeEmail;
                withBlock.From = De;
                if (Para != string.Empty)
                    withBlock.To.Add(Para);
                if (ComCopia != string.Empty)
                    withBlock.CC.Add(ComCopia);
                if (ComCopiaOculta != string.Empty)
                    withBlock.Bcc.Add(ComCopiaOculta);
                withBlock.Subject = Assunto;
                withBlock.IsBodyHtml = true;

                if (Logo != null)
                {
                    Stream LogoMarca = new MemoryStream(Logo);
                    System.Net.Mail.Attachment A = new Attachment(LogoMarca, "LogoMarca");
                    System.Random RGen = new Random();
                    A.ContentId = RGen.Next(100000, 9999999).ToString();
                    withBlock.Attachments.Add(A);
                    Corpo = "<table width='" + WidthPadrao + "' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td align='center'><img src='cid:" + A.ContentId + "' width='300' height='180'></td></tr></table><br>" + Corpo;
                }

                if (Anexos.Count > 0)
                {
                    for (int X = 0; X <= Anexos.Count - 1; X++)
                        withBlock.Attachments.Add(new Attachment(Anexos[X].ToString()));
                }
                withBlock.Body = Corpo;
                withBlock.BodyEncoding = CodificaoDoCorpo;
                withBlock.Priority = Prioridade;
            }
            try
            {
                objSmtp.Send(ClasseDeEmail);
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
        catch (Exception ex)
        {
            throw;
            return false;
        }
        finally
        {
            ClasseDeEmail.Dispose();
        }
    }

    public string ZEROS_A_ESQUERDA(string strCAMPO, int TAMANHO)
    {
        int CONTADOR = strCAMPO.Length;
        string RETORNO = strCAMPO;
        if (CONTADOR < TAMANHO)
        {
            for (int X = CONTADOR; X <= TAMANHO - 1; X++)
                RETORNO = "0" + RETORNO;
        }
        return RETORNO;
    }

    public bool ValidaCPF(string CPF)
    {
        if (CPF == "000.000.000-00" | CPF == "111.111.111-11" | CPF == "222.222.222-22" | CPF == "333.333.333-33" | CPF == "444.444.444-44" | CPF == "555.555.555-55" | CPF == "666.666.666-66" | CPF == "777.777.777-77" | CPF == "888.888.888-88" | CPF == "999.999.999-99")
            return false;

        // Caso coloque todos os numeros iguais
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        CPF = CPF.Trim();
        CPF = CPF.Replace(".", "").Replace("-", "");

        if (CPF.Length != 11)
            return false;

        tempCpf = CPF.Substring(0, 9);
        soma = 0;

        for (int i = 0; i <= 8; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i <= 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();
        return CPF.EndsWith(digito);
    }

    public bool ValidaCNPJ(string CNPJ)
    {
        if (CNPJ == "00.000.000/0000-00" | CNPJ == "11.111.111/1111-11" | CNPJ == "22.222.222/2222-22" | CNPJ == "33.333.333/3333-33" | CNPJ == "44.444.444/4444-44" | CNPJ == "55.555.555/5555-55" | CNPJ == "66.666.666/6666-66" | CNPJ == "77.777.777/7777-77" | CNPJ == "88.888.888/8888-88" | CNPJ == "99.999.999/9999-99")
            return false;

        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;

        CNPJ = CNPJ.Trim();
        CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

        if (CNPJ.Length != 14)
            return false;

        tempCnpj = CNPJ.Substring(0, 12);
        soma = 0;

        for (int i = 0; i <= 11; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        resto = (soma % 11);

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i <= 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return CNPJ.EndsWith(digito);
    }

    private bool EfetivaValidacao(string cnpj)
    {
        int[] Numero = new int[14];
        int soma;
        int i;
        int resultado1;
        int resultado2;

        for (i = 0; i <= Numero.Length - 1; i++)
            Numero[i] = System.Convert.ToInt32(cnpj.Substring(i, 1));

        soma = Numero[0] * 5 + Numero[1] * 4 + Numero[2] * 3 + Numero[3] * 2 + Numero[4] * 9 + Numero[5] * 8 + Numero[6] * 7 + Numero[7] * 6 + Numero[8] * 5 + Numero[9] * 4 + Numero[10] * 3 + Numero[11] * 2;

        soma = soma - (11 * int.Parse((soma / (double)11).ToString()));
        if (soma == 0 | soma == 1)
            resultado1 = 0;
        else
            resultado1 = 11 - soma;

        if (resultado1 == Numero[12])
        {
            soma = Numero[0] * 6 + Numero[1] * 5 + Numero[2] * 4 + Numero[3] * 3 + Numero[4] * 2 + Numero[5] * 9 + Numero[6] * 8 + Numero[7] * 7 + Numero[8] * 6 + Numero[9] * 5 + Numero[10] * 4 + Numero[11] * 3 + Numero[12] * 2;
            soma = soma - (11 * (int.Parse((soma / (double)11).ToString())));
            if (soma == 0 | soma == 1)
                resultado2 = 0;
            else
                resultado2 = 11 - soma;
            if (resultado2 == Numero[13])
                return true;
            else
                return true;
        }
        else
            return false;
    }


    public string GeraSenhaRandomica(int length)
    {
        var rand = new Random();
        StringBuilder password = new StringBuilder(length);

        for (int i = 1; i <= length; i++)
        {
            int charIndex;

            do
                charIndex = rand.Next(48, 123);
            while (!(charIndex >= 48 && charIndex <= 57) || (charIndex >= 65 && charIndex <= 90) || (charIndex >= 97 && charIndex <= 122));
            password.Append(Convert.ToChar(charIndex));
        }

        return password.ToString();
    }

    public static bool ValidaIE(string pUF, string pInscr)
    {
        bool retorno = false;
        string strBase;
        string strBase2;
        string strOrigem;
        string strDigito1;
        string strDigito2;
        int intPos;
        int intValor;
        int intSoma = 0;
        int intResto;
        int intNumero;
        int intPeso = 0;

        strBase = "";
        strBase2 = "";
        strOrigem = "";

        if ((pInscr.Trim().ToUpper() == "ISENTO"))
            return true;

        for (intPos = 1; intPos <= pInscr.Trim().Length; intPos++)
        {
            if ((("0123456789P".IndexOf(pInscr.Substring((intPos - 1), 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                strOrigem = (strOrigem + pInscr.Substring((intPos - 1), 1));
        }

        switch (pUF.ToUpper())
        {
            case "AC":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);

                    if (strBase.Substring(0, 2) == "01")
                    {
                        intSoma = 0;
                        intPeso = 4;

                        intPos = 1;
                        while ((intPos <= 11))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPeso == 1)
                                intPeso = 9;

                            intSoma += intValor * intPeso;

                            intPeso -= 1;
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                        intSoma = 0;
                        strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                        intPeso = 5;

                        intPos = 1;
                        while ((intPos <= 12))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPeso == 1)
                                intPeso = 9;

                            intSoma += intValor * intPeso;
                            intPeso -= 1;
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                        strBase2 = (strBase.Substring(0, 12) + strDigito2);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #End Region

                    break;
                    break;
                }

            case "AL":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "24"))
                    {
                        // 24000004-8
                        // 98765432
                        intSoma = 0;
                        intPeso = 9;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            intSoma += intValor * intPeso;
                            intPeso -= 1;
                            intPos += 1;
                        }

                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);

                        strDigito1 = ((intResto == 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto == 10) ? "0" : Convert.ToString(intResto)).Length - 1));

                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "AM":
                {

                    // #Region ""
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    intPeso = 9;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                        intSoma += intValor * intPeso;
                        intPeso -= 1;
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);

                    if (intSoma < 11)
                        strDigito1 = (11 - intSoma).ToString();
                    else
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #End Region

                    break;
                    break;
                }

            case "AP":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intPeso = 9;

                    if ((strBase.Substring(0, 2) == "03"))
                    {
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            intSoma += intValor * intPeso;
                            intPeso -= 1;
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));

                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "BA":
                {

                    // #Region ""

                    if (strOrigem.Length == 8)
                        strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                    else if (strOrigem.Length == 9)
                        strBase = (strOrigem.Trim() + "00000000").Substring(0, 9);

                    if ((("0123458".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                    {
                        // #Region ""

                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 6))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPos == 1)
                                intPeso = 7;

                            intSoma += intValor * intPeso;
                            intPeso -= 1;
                            intPos += 1;
                        }


                        intResto = (intSoma % 10);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));


                        strBase2 = strBase.Substring(0, 7) + strDigito2;

                        if (strBase2 == strOrigem)
                            retorno = true;

                        if (retorno)
                        {
                            intSoma = 0;
                            intPeso = 0;

                            intPos = 1;
                            while ((intPos <= 7))
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                                if (intPos == 7)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));

                                if (intPos == 1)
                                    intPeso = 8;

                                intSoma += intValor * intPeso;
                                intPeso -= 1;
                                intPos += 1;
                            }


                            intResto = (intSoma % 10);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));

                            strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);

                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }
                    }
                    else if ((("679".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                    {
                        // #Region ""

                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 6))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPos == 1)
                                intPeso = 7;

                            intSoma += intValor * intPeso;
                            intPeso -= 1;
                            intPos += 1;
                        }


                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));


                        strBase2 = strBase.Substring(0, 7) + strDigito2;

                        if (strBase2 == strOrigem)
                            retorno = true;

                        if (retorno)
                        {
                            intSoma = 0;
                            intPeso = 0;

                            intPos = 1;
                            while ((intPos <= 7))
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                                if (intPos == 7)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));

                                if (intPos == 1)
                                    intPeso = 8;

                                intSoma += intValor * intPeso;
                                intPeso -= 1;
                                intPos += 1;
                            }


                            intResto = (intSoma % 11);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                            strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);

                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }
                    }
                    else if ((("0123458".IndexOf(strBase.Substring(1, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 9)
                    {
                        // #Region ""
                        // Segundo digito 

                        // 1000003
                        // 8765432
                        intSoma = 0;


                        intPos = 1;
                        while ((intPos <= 7))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPos == 1)
                                intPeso = 8;

                            intSoma += intValor * intPeso;
                            intPeso -= 1;
                            intPos += 1;
                        }

                        intResto = (intSoma % 10);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));

                        strBase2 = strBase.Substring(0, 8) + strDigito2;

                        if (strBase2 == strOrigem)
                            retorno = true;

                        if (retorno)
                        {
                            // 1000003 6
                            // 9876543 2
                            intSoma = 0;
                            intPeso = 0;

                            intPos = 1;
                            while ((intPos <= 8))
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                                if (intPos == 8)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));

                                if (intPos == 1)
                                    intPeso = 9;

                                intSoma += intValor * intPeso;
                                intPeso -= 1;
                                intPos += 1;
                            }


                            intResto = (intSoma % 10);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                            strBase2 = (strBase.Substring(0, 7) + strDigito1 + strDigito2);

                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }
                    }

                    // #End Region

                    break;
                    break;
                }

            case "CE":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = 0;

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));

                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "DF":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);

                    if ((strBase.Substring(0, 3) == "073"))
                    {
                        intSoma = 0;
                        intPeso = 2;

                        intPos = 11;
                        while ((intPos >= 1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                            intPos = (intPos + -1);
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 11) + strDigito1);
                        intSoma = 0;
                        intPeso = 2;

                        intPos = 12;
                        while ((intPos >= 1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                            intPos = (intPos + -1);
                        }

                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 12) + strDigito2);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "ES":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "GO":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((("10,11,15".IndexOf(strBase.Substring(0, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);

                        if ((intResto == 0))
                            strDigito1 = "0";
                        else if ((intResto == 1))
                        {
                            intNumero = int.Parse(strBase.Substring(0, 8));
                            strDigito1 = (((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Substring(((((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Length - 1));
                        }
                        else
                            strDigito1 = Convert.ToString((11 - intResto)).Substring((Convert.ToString((11 - intResto)).Length - 1));

                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "MA":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "12"))
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "MT":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intSoma = 0;
                    intPeso = 2;

                    intPos = 10;
                    while (intPos >= 1)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 9))
                            intPeso = 2;
                        intPos = (intPos + -1);
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 10) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "MS":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "28"))
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "MG":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                    strBase2 = (strBase.Substring(0, 3) + ("0" + strBase.Substring(3, 8)));
                    intNumero = 2;

                    string strSoma = "";

                    intPos = 1;
                    while ((intPos <= 12))
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intNumero = ((intNumero == 2) ? 1 : 2);
                        intValor = (intValor * intNumero);

                        intSoma = (intSoma + intValor);
                        strSoma += intValor.ToString();
                        intPos += 1;
                    }

                    intSoma = 0;

                    // Soma -se os algarismos, não o produto
                    for (int i = 0; i <= strSoma.Length - 1; i++)
                        intSoma += int.Parse(strSoma.Substring(i, 1));

                    intValor = int.Parse(strBase.Substring(8, 2));
                    strDigito1 = (intValor - intSoma).ToString();

                    strBase2 = (strBase.Substring(0, 11) + strDigito1);

                    if ((strBase2 == strOrigem.Substring(0, 12)))
                        retorno = true;

                    if (retorno)
                    {
                        intSoma = 0;
                        intPeso = 3;

                        intPos = 1;
                        while ((intPos <= 12))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPeso < 2)
                                intPeso = 11;

                            intSoma += (intValor * intPeso);
                            intPeso -= 1;
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        intValor = 11 - intResto;
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                        strBase2 = (strBase.Substring(0, 12) + strDigito2);

                        if (strBase2 == strOrigem)
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "PA":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "15"))
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "PB":
                {

                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = 0;

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "PE":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "00000000000000").Substring(0, 14);
                    intSoma = 0;
                    intPeso = 2;

                    intPos = 7;
                    while ((intPos >= 1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 9))
                            intPeso = 2;
                        intPos = (intPos + -1);
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = (intValor - 10);

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 7) + strDigito1);

                    if ((strBase2 == strOrigem.Substring(0, 8)))
                        retorno = true;

                    if (retorno)
                    {
                        intSoma = 0;
                        intPeso = 2;

                        intPos = 8;
                        while ((intPos >= 1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                            intPos = (intPos + -1);
                        }

                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        if ((intValor > 9))
                            intValor = (intValor - 10);

                        strDigito2 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito2);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "PI":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "PR":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intSoma = 0;
                    intPeso = 2;

                    intPos = 8;
                    while ((intPos >= 1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 7))
                            intPeso = 2;
                        intPos = (intPos + -1);
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    intSoma = 0;
                    intPeso = 2;

                    intPos = 9;
                    while ((intPos >= 1))
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 7))
                            intPeso = 2;
                        intPos = (intPos + -1);
                    }

                    intResto = (intSoma % 11);
                    strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase2 + strDigito2);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "RJ":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                    intSoma = 0;
                    intPeso = 2;

                    intPos = 7;
                    while ((intPos >= 1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 7))
                            intPeso = 2;
                        intPos = (intPos + -1);
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 7) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "RN":
                {
                    // Verficar com 10 digitos
                    // #Region ""
                    if (strOrigem.Length == 9)
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    else if (strOrigem.Length == 10)
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 10);

                    if ((strBase.Substring(0, 2) == "20") && strBase.Length == 9)
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto > 9) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 9) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    else if (strBase.Length == 10)
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 9))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (11 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto > 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 10) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "RO":
                {
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    strBase2 = strBase.Substring(3, 5);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 5))
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intValor = (intValor * (7 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = (intValor - 10);

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    break;
                    break;
                }

            case "RR":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "24"))
                    {
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = intValor * intPos;
                            intSoma += intValor;
                            intPos += 1;
                        }

                        intResto = (intSoma % 9);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "RS":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intNumero = int.Parse(strBase.Substring(0, 3));

                    if (((intNumero > 0) && (intNumero < 468)))
                    {
                        intSoma = 0;
                        intPeso = 2;

                        intPos = 9;
                        while ((intPos >= 1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                            intPos = (intPos + -1);
                        }

                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        if ((intValor > 9))
                            intValor = 0;

                        strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }

            case "SC":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #End Region

                    break;
                    break;
                }

            case "SE":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    intPos = 1;
                    while ((intPos <= 8))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                        intPos += 1;
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = 0;

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "SP":
                {
                    // #Region ""

                    if ((strOrigem.Substring(0, 1) == "P"))
                    {
                        strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                        strBase2 = strBase.Substring(1, 8);
                        intSoma = 0;
                        intPeso = 1;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso == 2))
                                intPeso = 3;

                            if ((intPeso == 9))
                                intPeso = 10;
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + (strDigito1 + strBase.Substring(10, 3)));
                    }
                    else
                    {
                        strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                        intSoma = 0;
                        intPeso = 1;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso == 2))
                                intPeso = 3;

                            if ((intPeso == 9))
                                intPeso = 10;
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + (strDigito1 + strBase.Substring(9, 2)));
                        intSoma = 0;
                        intPeso = 2;

                        intPos = 11;
                        while ((intPos >= 1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 10))
                                intPeso = 2;
                            intPos = (intPos + -1);
                        }

                        intResto = (intSoma % 11);
                        strDigito2 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase2 + strDigito2);
                    }

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    // #End Region

                    break;
                    break;
                }

            case "TO":
                {
                    // #Region ""

                    strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);

                    if ((("01,02,03,99".IndexOf(strBase.Substring(2, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        strBase2 = (strBase.Substring(0, 2) + strBase.Substring(4, 6));
                        intSoma = 0;

                        intPos = 1;
                        while ((intPos <= 8))
                        {
                            intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                            intPos += 1;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 10) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    // #End Region

                    break;
                    break;
                }
        }

        return retorno;
    }
     
    public decimal PERCENTUAL(decimal mValorOriginal, decimal mPercentual, Acao mAcao)
    {
        switch (mAcao)
        {
            case Acao.Somar:
                {
                    return mValorOriginal + ((mValorOriginal * mPercentual) / (decimal)100);
                }

            case Acao.Subtrair:
                {
                    return mValorOriginal - ((mValorOriginal * mPercentual) / (decimal)100);
                }

            case Acao.Retorno:
                {
                    return (mValorOriginal * mPercentual) / (decimal)100;
                }
            default:
                return 0;
        }
    }

    public enum Acao : int
    {
        Retorno,
        Somar,
        Subtrair
    }
    
    //public decimal ArredondarMoeda(decimal Valor)
    //{
    //    int iPos;     // Declara as variáveis
    //    // Verifica qual a posição da vírgula dentro da variável
    //    iPos = Strings.InStr(1, System.Convert.ToString(Valor), ",");
    //    // Se não há vígula o número é inteiro, então a posição é o tamanho da string
    //    if (iPos == 0)
    //        iPos = Strings.Len(System.Convert.ToString(Valor));
    //    // Retorna o número até a segunda casa decimal
    //    return System.Convert.ToDecimal(Strings.Mid(System.Convert.ToString(Valor), 1, iPos + 2));
    //}

    public string QuotedStr(string pValor)
    {
        return "'" + pValor + "'";
    }

    public string VerificaNULL(string Texto, int Tipo)
    {
        switch (Tipo)
        {
            case 0:
                {
                    if (Texto.Trim() == "")
                        return "NULL";
                    else
                        return Texto.Trim();
                    break;
                }

            case 1:
                {
                    if (Texto.Trim() == "")
                        return "NULL";
                    else
                        return QuotedStr(Texto.Trim());
                    break;
                }

            case 2:
                {
                    if (Texto.Trim() == "")
                        return "";
                    else
                        return (Texto.Trim());
                    break;
                }

            default:
                {
                    return "";
                }
        }
    }

    public bool ValidaEstado(string Dado)
    {
        const string Estados = "SPMGRJRSSCPRESDFMTMSGOTOBASEALPBPEMARNCEPIPAAMAPFNACRRRO";
        int Posicao;
        bool Result;

        Result = true;
        if (Dado != "")
        {
            Posicao = Estados.IndexOf(Dado.ToUpper());
            if ((Posicao == 0) || ((Posicao % 2) == 0))
                Result = false;
        }
        return Result;
    }

    private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
    private TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

    public string MD5File(string file__1)
    {
        using (FileStream stream = File.OpenRead(file__1))
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checksum = md5.ComputeHash(stream);
            return (BitConverter.ToString(checksum)).Replace("-", string.Empty);
        }
    }

    private byte[] MD5Hash(string valor)
    {
        return md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(valor));
    }

    public string MD5String(string texto, string Chave = "1324567890qwertyuiopásdfghjklç~zxcvbnm,.;")
    {
        des.Key = MD5Hash(Chave);
        des.Mode = CipherMode.ECB;
        byte[] buffer = md5.ComputeHash(Encoding.ASCII.GetBytes(texto));
        return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
    }

    public decimal TruncaValor(decimal Value, int Casas)
    {
        string sValor;
        int nPos;

        sValor = Value.ToString();

        nPos = sValor.IndexOf(",");
        if (nPos > 0)
            sValor = sValor.Substring(0, nPos + Casas + 1);

        return Convert.ToDecimal(sValor);
    }

    public decimal TruncaValor(System.Nullable<decimal> Value, int Casas)
    {
        string sValor;
        int nPos;

        sValor = Value.ToString();

        nPos = sValor.IndexOf(",");
        if (nPos > 0)
            sValor = sValor.Substring(0, nPos + Casas);

        return Convert.ToDecimal(sValor);
    }

    public string FormataFloat(string Tipo, decimal Valor, int DECIMAIS_QUANTIDADE = 3, int DECIMAIS_VALOR = 2)
    {
        int i;
        string Mascara;

        Mascara = "0.";

        if (Tipo == "Q")
        {
            for (i = 1; i <= DECIMAIS_QUANTIDADE; i++)
                Mascara = Mascara + "0";
        }
        else if (Tipo == "V")
        {
            for (i = 1; i <= DECIMAIS_VALOR; i++)
                Mascara = Mascara + "0";
        }

        return Convert.ToDecimal(Valor).ToString(Mascara);
    }

    public string FormataFloat(string Tipo, System.Nullable<decimal> Valor, int DECIMAIS_QUANTIDADE = 3, int DECIMAIS_VALOR = 2)
    {
        int i;
        string Mascara;

        Mascara = "0.";

        if (Tipo == "Q")
        {
            for (i = 1; i <= DECIMAIS_QUANTIDADE; i++)
                Mascara = Mascara + "0";
        }
        else if (Tipo == "V")
        {
            for (i = 1; i <= DECIMAIS_VALOR; i++)
                Mascara = Mascara + "0";
        }

        return Convert.ToDecimal(Valor).ToString(Mascara);
    }

    public string DevolveConteudoDelimitado(string Delimidador, string Linha)
    {
        int PosBarra;
        string Result;

        PosBarra = Linha.IndexOf(Delimidador);
        Result = (Linha.Substring(PosBarra - 1, 1)).Replace("[#]", "|");
        Linha = Linha.Remove(0, PosBarra);
        return Result;
    }

    public string TiraPontos(string Str)
    {
        int i;
        string xStr;
        string Result;

        xStr = "";
        for (i = 1; i <= Str.Trim().Length; i++)
        {
            if (("/-.)(,".IndexOf(Str.Substring(1, i)) == 0))
                xStr = xStr + Str[i];
        }

        xStr = xStr.Replace(" ", "");

        Result = xStr;
        return Result;
    }

    public static DateTime TextoParaData(string pData)
    {
        int Dia;
        int Mes;
        int Ano;
        System.DateTime Result;

        // formato de entrada'yyyyMMdd
        if ((pData != "") && (pData != "00000000"))
        {
            Dia = Convert.ToInt32(pData.Substring(6, 2));
            Mes = Convert.ToInt32(pData.Substring(4, 2));
            Ano = Convert.ToInt32(pData.Substring(0, 4));
            Result = new DateTime(Ano, Mes, Dia);
        }
        else
            Result = new DateTime();
        return Result;
    }

    public string DataParaTexto(DateTime pData)
    {
        return pData.ToString("yyyy-MM-dd");
    }

    public string Encripta(string pChave)
    {
        string chaveCriptografada = pChave;
        if (pChave != "")
        {
            Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(pChave); // (Criptog(pChave))
            chaveCriptografada = Convert.ToBase64String(b);
        }
        return chaveCriptografada;
    }

    public string Desencripta(string pChave)
    {
        string chaveDecriptografada = pChave;
        if (pChave != "")
        {
            Byte[] b = Convert.FromBase64String(pChave); // (DsCriptog(pChave))
            chaveDecriptografada = System.Text.ASCIIEncoding.ASCII.GetString(b);
        }
        return chaveDecriptografada;
    }
       
    public string NumeroParaExtenso(decimal number)
    {
        int cent;
        try
        {
            if (number == 0)
                return "Zero Reais";
            cent = (int)Math.Round((number - int.Parse(number.ToString())) * 100, MidpointRounding.ToEven);
            number = int.Parse(number.ToString());
            if (cent > 0)
            {
                if (number == 1)
                    return "Um Real e " + ConverteParteDecimal((byte)cent) + "centavos";
                else if (number == 0)
                    return ConverteParteDecimal((byte)cent) + "centavos";
                else
                    return ConverteParteInteira(number) + "Reais e " + ConverteParteDecimal((byte)cent) + "centavos";
            }
            else
                if (number == 1)
                return "Um Real";
            else
                return ConverteParteInteira(number) + "Reais";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string ConverteParteDecimal(byte number)
    {
        try
        {
            switch (number)
            {
                case 0:
                    {
                        return "";
                    }

                case object _ when 1 <= number && number <= 19:
                    {
                        string[] strArray = new[] { "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezasseis", "Dezessete", "Dezoito", "Dezenove" };
                        return strArray[number - 1] + " ";
                    }

                case object _ when 20 <= number && number <= 99:
                    {
                        string[] strArray = new[] { "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
                        if ((number % 10) == 0)
                            return strArray[number / 10 - 2] + " ";
                        else
                            return strArray[number / 10 - 2] + " e " + ConverteParteDecimal((byte)(number % 10)) + " ";
                        break;
                    }

                default:
                    {
                        return "";
                    }
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string ConverteParteInteira(decimal number)
    {
        try
        {
            switch (number)
            {
                case object _ when number < 0:
                    {
                        return "-" + ConverteParteInteira(-number);
                    }

                case 0:
                    {
                        return "";
                    }

                case object _ when 1 <= number && number <= 19:
                    {
                        string[] strArray = new[] { "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezasseis", "Dezessete", "Dezoito", "Dezenove" };
                        return strArray[(int)number - 1] + " ";
                    }

                case object _ when 20 <= number && number <= 99:
                    {
                        string[] strArray = new[] { "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
                        if ((number % 10) == 0)
                            return strArray[(int)number / 10 - 2];
                        else
                            return strArray[(int)number / 10 - 2] + " e " + ConverteParteInteira(number % 10);
                        break;
                    }

                case 100:
                    {
                        return "Cem";
                    }

                case object _ when 101 <= number && number <= 999:
                    {
                        string[] strArray = new[] { "Cento", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };
                        if ((number % 100) == 0)
                            return strArray[(int)number / 100 - 1] + " ";
                        else
                            return strArray[(int)number / 100 - 1] + " e " + ConverteParteInteira(number % 100);
                        break;
                    }

                case object _ when 1000 <= number && number <= 1999:
                    {
                        switch ((number % 1000))
                        {
                            case 0:
                                {
                                    return "Mil";
                                }

                            case object _ when (number % 1000) <= 100:
                                {
                                    return "Mil e " + ConverteParteInteira(number % 1000);
                                }

                            default:
                                {
                                    return "Mil, " + ConverteParteInteira(number % 1000);
                                }
                        }

                        break;
                    }

                case object _ when 2000 <= number && number <= 999999:
                    {
                        switch ((number % 1000))
                        {
                            case 0:
                                {
                                    return ConverteParteInteira(number / 1000) + "Mil";
                                }

                            case object _ when (number % 1000) <= 100:
                                {
                                    return ConverteParteInteira(number / 1000) + "Mil e " + ConverteParteInteira(number % 1000);
                                }

                            default:
                                {
                                    return ConverteParteInteira(number / 1000) + "Mil, " + ConverteParteInteira(number % 1000);
                                }
                        }

                        break;
                    }

                case object _ when 1000000 <= number && number <= 1999999:
                    {
                        switch ((number % 1000000))
                        {
                            case 0:
                                {
                                    return "Um Milhão";
                                }

                            case object _ when (number % 1000000) <= 100:
                                {
                                    return ConverteParteInteira(number / 1000000) + "Milhão e " + ConverteParteInteira(number % 1000000);
                                }

                            default:
                                {
                                    return ConverteParteInteira(number / 1000000) + "Milhão, " + ConverteParteInteira(number % 1000000);
                                }
                        }

                        break;
                    }

                case object _ when 2000000 <= number && number <= 999999999:
                    {
                        switch ((number % 1000000))
                        {
                            case 0:
                                {
                                    return ConverteParteInteira(number / 1000000) + " Milhões";
                                }

                            case object _ when (number % 1000000) <= 100:
                                {
                                    return ConverteParteInteira(number / 1000000) + "Milhões e " + ConverteParteInteira(number % 1000000);
                                }

                            default:
                                {
                                    return ConverteParteInteira(number / 1000000) + "Milhões, " + ConverteParteInteira(number % 1000000);
                                }
                        }

                        break;
                    }

                case object _ when 1000000000 <= number && number <= 1999999999:
                    {
                        switch ((number % 1000000000))
                        {
                            case 0:
                                {
                                    return "Um Bilhão";
                                }

                            case object _ when (number % 1000000000) <= 100:
                                {
                                    return ConverteParteInteira(number / 1000000000) + "Bilhão e " + ConverteParteInteira(number % 1000000000);
                                }

                            default:
                                {
                                    return ConverteParteInteira(number / 1000000000) + "Bilhão, " + ConverteParteInteira(number % 1000000000);
                                }
                        }

                        break;
                    }

                default:
                    {
                        switch ((number % 1000000000))
                        {
                            case 0:
                                {
                                    return ConverteParteInteira(number / 1000000000) + " Bilhões";
                                }

                            case object _ when (number % 1000000000) <= 100:
                                {
                                    return ConverteParteInteira(number / 1000000000) + "Bilhões e " + ConverteParteInteira(number % 1000000000);
                                }

                            default:
                                {
                                    return ConverteParteInteira(number / 1000000000) + "Bilhões, " + ConverteParteInteira(number % 1000000000);
                                }
                        }

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string PrimeiraMaiuscula(String strString)
    {
        string strResult = "";
        short firstChar;
        if (strString.Length > 0)
        {
            if (strString.Substring(0, 1) == "&")
                firstChar = 1;
            else
                firstChar = 0;
            strResult += strString.Substring(firstChar, 1).ToUpper();
            strResult += strString.Substring(firstChar + 1, strString.Length - (firstChar + 1)).ToLower();
        }
        return strResult;
    }

    public string PrimeiraMaiusculaTodasPalavras(String strString)
    {
        string strResult = "";
        bool booPrimeira = true;
        if (strString.Length > 0)
        {
            for (int intCont = 0; intCont <= strString.Length - 1; intCont++)
            {
                if ((booPrimeira) && (!strString.Substring(intCont, 1).Equals(" ")))
                {
                    strResult += strString.Substring(intCont, 1).ToUpper();
                    booPrimeira = false;
                }
                else
                {
                    strResult += strString.Substring(intCont, 1).ToLower();
                    if (strString.Substring(intCont, 1).Equals(" "))
                        booPrimeira = true;
                }
            }
        }
        return strResult;
    }
}
