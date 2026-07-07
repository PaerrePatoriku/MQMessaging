namespace MQCommon.Customs;

/*
 * Imaginary codes for a customs system. This specifies a message type basically
 * For example, the finnish customs system uses codes that start with FI, then
 * a number that usually denotes a system type (import, export, statistic etc), then
 * two numbers that specify the message type within a system.
 *
 *  My system is completely imaginary, so I will put some random values here that kind of make sense either way...
 */
public enum MessageType
{
  CUS_DECLARATION_REGISTERED, //message entered the customs system OK and did pass validation
  CUS_ACCEPTED, //the declaration was OK'd by customs handlers
  CUS_REJECTED, //as in the message was OK, but in the real world there are issues outside the validation. (customs rules outside validation for example...)
  CUS_CUSTOMER_RESPONSE_REQUIRED, //Meaning that the imaginary customs system needs extra info for a declaration
  CUS_VALIDATION_ERROR //the message was not OK and did not match the customs system contract
  //could add a bunch more here I guess but lets just have these for the simulation...
  
}